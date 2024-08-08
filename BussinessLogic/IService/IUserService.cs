﻿using BussinessLogic.DTO;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.IService
{
    public interface IUserService
    {
        List<UserDTO> GetUsers();
        UserDTO AuthenticationUser(string userName, string password);
        void AddUser(User us);
        UserDTO GetUserById(int id);
        void ChangePass(ChangePasswordRequest request);
        List<string> ReportManage(int months);
    }
}
