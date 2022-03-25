using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSMotoCycle.Models;
using VSMotoCycle.Storage;

namespace VSMotoCycle.Controllers
{
    public class UserController : Controller
    {
        public string message { get; set; }
        private bool success { get; set; }
        private readonly IUserService _userService;
        private readonly IAuthStorage _authStorage;
        private readonly INotyfService _notyfService;

       

        public UserController(IUserService userService, IAuthStorage authStorage, INotyfService notyfService)
        {
            _userService = userService;
            _authStorage = authStorage;
            _notyfService = notyfService;
        }

        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUser(UserAddDto user)
        {
            var result = _userService.Add(user);
            if (result.Success)
            {
                _notyfService.Success(result.Message);
            }
            else
            {
                _notyfService.Warning(result.Message);
            }
            return View();
        }
        public IActionResult UpdateUser(int userId)
        {
            var result = _userService.GetUserDtoByUserId(userId);
            var model = new UserModel();
            model.UserDto = result.Data;
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateUser(UserAddDto user)
        {

            var result = _userService.Update(user);
            if (result.Success)
            {
                _notyfService.Success(result.Message);
            }
            else
            {
                _notyfService.Warning(result.Message);
            }

            return UpdateUser(user.UserId);
        }
        public IActionResult GetAllUser()
        {
            var model = new UserModel();
            var user = _authStorage.GetUser();
            if (user != null)
            {
                if (user.OperationClaimId == 1)
                {
                    model.UserList = _userService.GetUserDtoList().Data;
                }
                if (message != null)
                {
                    if (success)
                    {
                        _notyfService.Success(message);
                    }
                    else
                    {
                        _notyfService.Warning(message);
                    }
                }
                return View(model);
            }
            Response.Redirect("../Auth/Login");
            return View(model);
        }
        [HttpPost]
        public IActionResult DeleteUser(int userId)
        {
            var user = _authStorage.GetUser();
            if (user != null)
            {
                if (user.OperationClaimId == 1)
                {
                    var result = _userService.Delete(userId);
                    if (result.Success)
                    {
                        _notyfService.Success(result.Message);
                    }
                    else
                    {
                        _notyfService.Warning(result.Message);
                    }
                }
                Response.Redirect("GetAllUser");
                return View();
            }
            Response.Redirect("../Auth/Login");
            return View();

        }

     
    }
}
