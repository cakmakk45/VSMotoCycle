using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VSMotoCycle.Models;
using VSMotoCycle.Storage;

namespace VSMotoCycle.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthStorage _authStorage;
        public HomeController(ILogger<HomeController> logger, IAuthStorage authStorage)
        {
            _logger = logger;
            _authStorage = authStorage;
        }

        public IActionResult Index()
        {
            var userModel = new UserModel();
            userModel.UserDto = _authStorage.GetUser();
            return View(userModel);
        }

        public IActionResult Privacy()
        {
            if (_authStorage.GetUser() ==null)
            {
                Response.Redirect("Auth/Login");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
