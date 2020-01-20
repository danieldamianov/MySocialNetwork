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
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;

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

            System.IO.File.WriteAllBytes("Photos/test.txt", new byte[] { 1, 2, 3, 4 });


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

        [Authorize]
        [HttpPost]
        [ActionName("UploadPhoto")]
        public async Task<IActionResult> HandleUploadingPhoto(List<IFormFile> files)
        {
            using (var stream = new FileStream("Photos/1.jpg", FileMode.Create))
            {
                await files[0].CopyToAsync(stream);
            }

            return Ok(new { count = 1, files[0].Length });

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

