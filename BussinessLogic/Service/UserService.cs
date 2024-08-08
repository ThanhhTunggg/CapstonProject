﻿using AutoMapper;
using BussinessLogic.DTO;
using BussinessLogic.IService;
using DataAccess.DAO;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserDAO _userDAO;
        public UserService(IMapper mapper, UserDAO userDAO)
        {
            _mapper = mapper;
            _userDAO = userDAO;
        }

        public List<UserDTO> GetUsers()
        {
            return _mapper.Map<List<UserDTO>>(_userDAO.GetUsers());
        }
        public UserDTO AuthenticationUser(string userName, string password)
        {
            return _mapper.Map<UserDTO>(_userDAO.Authentication(userName, password));
        }

        public UserDTO GetUserById(int id)
        {
            return _mapper.Map<UserDTO>(_userDAO.GetUserById(id));
        }

        public void ChangePass(ChangePasswordRequest request)
        {
            using (DataAccessContext context = new DataAccessContext())
            {
                if (request != null)
                {
                    if (request.NewPass == request.ReNewPass)
                    {
                        var data = context.Users.FirstOrDefault(x => x.UserId == request.UserId);
                        if (data != null)
                        {
                            data.UserPassword = request.NewPass;
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public List<User> GetAllUser()
        {
            using (DataAccessContext context = new DataAccessContext())
            {
                return context.Users.ToList();
            }
        }

        public List<string> ReportManage(int months)
        {
            DateTime fromDate = DateTime.Now.AddMonths(-months);
            var rst = new List<string>();
            using (DataAccessContext context = new DataAccessContext())
            {
                var total1 = context.Orders.Where(p => p.OrderDate >= fromDate).ToList();
                rst.Add(total1.Count().ToString());
                double total2 = 0;
                foreach (var item in total1)
                {
                    total2 += item.TotalMoney;
                }
                rst.Add(total2.ToString());

                var mostFrequentProductId = context.OrderDetails
                                            .GroupBy(od => od.ProductId)
                                            .OrderByDescending(g => g.Count())
                                            .Select(g => new
                                            {
                                                ProductId = g.Key,
                                                Count = g.Count()
                                            })
                                            .FirstOrDefault();
                var data = context.Products.FirstOrDefault(x => x.ProductId == mostFrequentProductId.ProductId);
                rst.Add(data.ProductName);
                return rst;
            }
        }

        public void AddUser(User us)
        {
            using (DataAccessContext context = new DataAccessContext())
            {
                context.Users.Add(us);
                context.SaveChanges();
            }
        }
    }
}
