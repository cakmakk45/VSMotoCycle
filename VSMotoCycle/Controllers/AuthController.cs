using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSMotoCycle.Storage;

namespace VSMotoCycle.Controllers
{
    public class AuthController : Controller
    {
        private IAuthService _authService;
        private readonly INotyfService _notyfService;
        private readonly IAuthStorage _authStorage;



        public AuthController(IAuthService authService, INotyfService notyfService, IAuthStorage authStorage)
        {
            _authService = authService;
            _notyfService = notyfService;
            _authStorage = authStorage;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var result = _authService.Login(userForLoginDto);
            if (result.Success)
            {
                if (result.Data.OperationClaimId==1)
                {
                    _authStorage.SetUser(result.Data);
                    _notyfService.Success(result.Message);
                    Response.Redirect("../Home/privacy");
                }
                else
                {
                    _notyfService.Error("Sadece Yönetici üyeler giriş yapabilir.");
                }

            }
            else
            {
                _notyfService.Error(result.Message);
            }
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserForRegisterDto user)
        {
            var result = _authService.Register(user);
            if (result.Success)
            {
                _notyfService.Success(result.Message);
            }
            else
            {
                _notyfService.Error(result.Message);
            }
            return View();
        }
        [HttpPost]
        public IActionResult LoginOut()
        {
            _authStorage.removeUser();
            _notyfService.Success("Çıkış Başarılı");
            Response.Redirect("../Auth/Login");
            return Login();
        }
    }
}
