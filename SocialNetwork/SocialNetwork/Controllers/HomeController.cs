using System;
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
using SocialNetwork.Controllers.ImageConvertingFunctionality;
using System.Linq;
using SocialNetwork.Services.FunctionalityForProfileManagement;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UsersFollowingFunctionalityService usersFollowingFunctionalityService;

        private readonly UsersPostsService usersPostsService;

        private readonly ImageConverter imageConverter;

        private readonly ProfileManagementService profileManagementService;

        public HomeController(ILogger<HomeController> logger,
            UsersFollowingFunctionalityService usersFollowingFunctionalityService,
            UsersPostsService usersPostsService,
            ImageConverter imageConverter,
            ProfileManagementService profileManagementService)
        {
            _logger = logger;
            this.usersFollowingFunctionalityService = usersFollowingFunctionalityService;
            this.usersPostsService = usersPostsService;
            this.imageConverter = imageConverter;
            this.profileManagementService = profileManagementService;

            
        }

        //private void SetProfileLinkDAta()
        //{
        //    byte[] photo = this.profileManagementService
        //        .GetUserProfileLinkById(this.GetUserId()).Photo;
        //    if (photo != null)
        //    {
        //        this.ViewData["profileImageCode"] = imageConverter.ConvertByteArrayToString(photo); 
        //    }
        //    else
        //    {
        //        this.ViewData["profileImageCode"] = imageConverter.ConvertByteArrayToString(System.IO.File.ReadAllBytes("wwwroot/pics/logo.png"));
        //    }
        //}

        public IActionResult Index()
        {
            NewsFeedHomeIndexViewModel newsFeedHomeIndexViewModel = new NewsFeedHomeIndexViewModel();
            AddUserToDatabase();
            if (this.User.Identity.IsAuthenticated)
            {
                string username = this.User.Identity.Name;
                this.ViewData["username"] = username;
                //SetProfileLinkDAta();

                List<ImagePostDTO> imagePostsOfFollowingUsers =
                    this.usersPostsService.GetAllImagePostsOfGivenUsersIds
                    (this.usersFollowingFunctionalityService
                    .GetUsersIdsWhichGivenUserFollows(GetUserId()));


                foreach (var post in imagePostsOfFollowingUsers)
                {
                    string imgDataURL = this.imageConverter.ConvertByteArrayToString(post.Photo);
                    newsFeedHomeIndexViewModel.Posts.Add(new PostHomeIndexViewModel
                    {
                        Description = post.Description,
                        Code = imgDataURL,
                        Username = post.Username,
                        DateTimeCreated = post.DateTimeCreated,
                        PostId = post.PostId,
                        Comments = post.Comments.Select(comment => new CommentHomeIndexViewModel(comment.Content,comment.Username)).ToList()
                    });
                }

            }

            newsFeedHomeIndexViewModel.Posts = newsFeedHomeIndexViewModel.Posts.OrderByDescending(post => post.DateTimeCreated).ToList();
            return View(newsFeedHomeIndexViewModel);
        }

        
        private string GetUserId()
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

        [Authorize]
        public IActionResult NewPost()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        [ActionName("NewPost")]
        public async Task<IActionResult> NewPostProcessingData(List<IFormFile> files, string description)
        {
            using (var stream = new MemoryStream())
            {
                await files[0].CopyToAsync(stream);

                stream.Seek(0, SeekOrigin.Begin);

                this.usersPostsService.AddPostToUser(this.User.FindFirstValue(ClaimTypes.NameIdentifier)
                    , stream.ToArray(), description);
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

