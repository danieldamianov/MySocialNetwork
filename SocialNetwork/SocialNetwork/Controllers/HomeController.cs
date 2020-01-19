using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork.Models;
using SocialNetwork.Services;
using System.Security.Claims;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UsersFollowingFunctionalityService UsersFollowingFunctionalityService;

        public HomeController(ILogger<HomeController> logger,
            UsersFollowingFunctionalityService usersFollowingFunctionalityService)
        {
            _logger = logger;
            UsersFollowingFunctionalityService = usersFollowingFunctionalityService;
        }


        public IActionResult Index()
        {
            AddUserToDatabase();
            string username = this.User.Identity.Name;
            this.ViewData["username"] = username;
            return View();
        }

        private void AddUserToDatabase()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                this.UsersFollowingFunctionalityService.AddUser(userId, this.User.Identity.Name);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult UploadPhoto()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
