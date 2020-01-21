﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork.Models;
using SocialNetwork.Services.FunctionalityForFollowingAndFollowedUsers;
using System.Security.Claims;
using System.IO;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Services.FuctionalityForManagementOfPosts;
using SocialNetwork.Services.FuctionalityForManagementOfPosts.DbTransferObjects;
using SocialNetwork.Models.Home;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UsersFollowingFunctionalityService usersFollowingFunctionalityService;

        private readonly UsersPostsService usersPostsService;

        public HomeController(ILogger<HomeController> logger,
            UsersFollowingFunctionalityService usersFollowingFunctionalityService,
            UsersPostsService usersPostsService)
        {
            _logger = logger;
            this.usersFollowingFunctionalityService = usersFollowingFunctionalityService;
            this.usersPostsService = usersPostsService;
        }


        public IActionResult Index()
        {
            NewsFeedHomeIndexViewModel newsFeedHomeIndexViewModel = new NewsFeedHomeIndexViewModel();
            AddUserToDatabase();
            if (this.User.Identity.IsAuthenticated)
            {
                string username = this.User.Identity.Name;
                this.ViewData["username"] = username;

                List<ImagePostDTO> imagePostsOfFollowingUsers =
                    this.usersPostsService.GetAllImagePostsOfGivenUsersIds
                    (this.usersFollowingFunctionalityService
                    .GetUsersIdsWhichGivenUserFollows(GetUserId()));


                foreach (var post in imagePostsOfFollowingUsers)
                {
                    string imgeBase64Data = Convert.ToBase64String(post.Photo);
                    string imgDataURL = string.Format("data:image/jpg;base64,{0}", imgeBase64Data);
                    newsFeedHomeIndexViewModel.Posts.Add(new PostHomeIndexViewModel
                    {
                        Code = imgDataURL,
                    });
                }

            }


            return View(newsFeedHomeIndexViewModel);
        }

        public string GetUserId()
        {
            return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private void AddUserToDatabase()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                this.usersFollowingFunctionalityService.AddUser(userId, this.User.Identity.Name);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult NewPost()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        [ActionName("NewPost")]
        public async Task<IActionResult> NewPostProcessingData(List<IFormFile> files)
        {
            using (var stream = new MemoryStream())
            {
                await files[0].CopyToAsync(stream);

                stream.Seek(0, SeekOrigin.Begin);

                this.usersPostsService.AddPostToUser(this.User.FindFirstValue(ClaimTypes.NameIdentifier)
                    , stream.ToArray(), string.Empty);
            }


            return this.Redirect("/");

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

