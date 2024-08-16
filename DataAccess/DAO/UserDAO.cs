using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class UserDAO
    {
        public IEnumerable<User> GetUsers()
        {
            using (DataAccessContext context = new DataAccessContext())
            {
                return context.Users.ToList();
            }
        }

        public User Authentication(string username, string password)
        {
            using (DataAccessContext context = new DataAccessContext())
            {
                return context.Users.FirstOrDefault(x => x.UserEmail.Equals(username) && x.UserPassword.Equals(password) && (x.UserRole <= 2 || x.UserRole == 4));
            }

        }
        public User GetUserById(int id)
        {
            using (DataAccessContext context = new DataAccessContext())
            {
                return context.Users.FirstOrDefault(x => x.UserId == id);
            }
        }

        public bool ValidateUser(int usId, int token)
        {
            using (DataAccessContext context = new DataAccessContext())
            {
                var data = context.Users.FirstOrDefault(x => x.UserId == usId && x.Token == token);
                if(data != null)
                {
                    data.Token = 0;
                    data.UserRole = 2;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
