﻿using AutoMapper;
using BussinessLogic.DTO;
using BussinessLogic.IService;
using BussinessLogic.Service;
using DataAccess.Model;
using Microsoft.AspNetCore.Mvc;

namespace ShopManageOnline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IMapper mapper, IUserService userService)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetUsers")]
        public ActionResult<UserDTO> Get()
        {
            try
            {
                List<UserDTO> listUsers = _userService.GetUsers();
                if (listUsers == null)
                {
                    return NotFound();
                }
                return Ok(listUsers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddUsers")]
        public ActionResult AddUser([FromBody] User us)
        {
            try
            {
                _userService.AddUser(us);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUserById/{UserId}")]
        public ActionResult<UserDTO> GetUsersById(int UserId)
        {
            try
            {
                UserDTO listUsers = _userService.GetUserById(UserId);
                if (listUsers == null)
                {
                    return NotFound();
                }
                return Ok(listUsers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "Authentication")]
        public ActionResult<UserDTO> Authentication(string userEmail, string password)
        {
            try
            {
                UserDTO listUsers = _userService.AuthenticationUser(userEmail, password);
                if (listUsers == null)
                {
                    return NotFound();
                }
                return Ok(listUsers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ChangePass")]
        public ActionResult ChangePass([FromBody] ChangePasswordRequest request)
        {
            try
            {
                _userService.ChangePass(request);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("report/{mounts}")]
        public ActionResult GetReport(int mounts)
        {
            try
            {
                return Ok(_userService.ReportManage(mounts));
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}
