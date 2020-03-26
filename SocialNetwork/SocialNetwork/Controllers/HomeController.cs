using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Controllers.Extensions;
using SocialNetwork.Controllers.ImageConvertingFunctionality;
using SocialNetwork.Controllers.TimeSinceCreationFunctionality;
using SocialNetwork.InputViewModels.Home;
using SocialNetwork.Models;
using SocialNetwork.Models.Home;
using SocialNetwork.Services.FollowingManagement;
using SocialNetwork.Services.LikesManagement;
using SocialNetwork.Services.PostsManagement;
using SocialNetwork.Services.PostsManagement.DTOs;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFollowingService usersFollowingFunctionalityService;

        private readonly IUsersPostsService usersPostsService;

        private readonly ImageConverter imageConverter;

        private readonly IControllerAdditionalFunctionality controllerAdditionalFunctionality;

        private readonly TimeConvertingService timeConvertingService;

        private readonly ILikesService likesService;

        private readonly IWebHostEnvironment env;

        public HomeController(
            IFollowingService usersFollowingFunctionalityService,
            IUsersPostsService usersPostsService,
            ImageConverter imageConverter,
            IControllerAdditionalFunctionality controllerAdditionalFunctionality,
            TimeConvertingService timeConvertingService,
            ILikesService likesService,
            IWebHostEnvironment env)
        {
            this.usersFollowingFunctionalityService = usersFollowingFunctionalityService;
            this.usersPostsService = usersPostsService;
            this.imageConverter = imageConverter;
            this.controllerAdditionalFunctionality = controllerAdditionalFunctionality;
            this.timeConvertingService = timeConvertingService;
            this.likesService = likesService;
            this.env = env;
        }

        public IActionResult Index()
        {
            NewsFeedHomeIndexViewModel newsFeedHomeIndexViewModel = new NewsFeedHomeIndexViewModel();

            if (this.User.Identity.IsAuthenticated)
            {
                string username = this.User.Identity.Name;
                newsFeedHomeIndexViewModel.Username = username;

                List<ImagePostDTO> imagePostsOfFollowingUsers =
                    this.usersPostsService.GetAllImagePostsOfGivenUsersIds
                    (this.usersFollowingFunctionalityService
                    .GetUsersIdsWhichGivenUserFollows(GetUserId()))
                    .OrderByDescending(p => p.DateTimeCreated)
                    .ToList();


                foreach (var post in imagePostsOfFollowingUsers)
                {
                    string imgDataURL = this.imageConverter.ConvertByteArrayToString(post.Photo);
                    List<UserLikedPostHomeIndexViewModel> usersWhoLikeTheCurrentPost = this.likesService.GetPeopleWhoLikePost(post.PostId).Select
                        (
                            user => new UserLikedPostHomeIndexViewModel(user.UserName, user.Id,
                            this.controllerAdditionalFunctionality.GetProfilePicture(user.Id))
                        ).ToList();

                    newsFeedHomeIndexViewModel.Posts.Add(new PostHomeIndexViewModel
                    {
                        Description = post.Description,
                        Code = imgDataURL,
                        Username = post.Username,
                        TimeSinceCreated = this.timeConvertingService.ConvertDateTime(post.DateTimeCreated),
                        PostId = post.PostId,
                        Comments = post.Comments.Select(comment => new CommentHomeIndexViewModel(comment.Content, comment.Username,
                        comment.UserId, this.controllerAdditionalFunctionality.GetProfilePicture(comment.UserId))).ToList(),
                        UserAvatarCode = this.controllerAdditionalFunctionality.GetProfilePicture(post.CreatorId),
                        UserId = post.CreatorId,
                        UsersLikedThePost = usersWhoLikeTheCurrentPost,
                        HasCurrentUserLikedThePost = usersWhoLikeTheCurrentPost.Any(user => user.Id == this.GetUserId()),
                        LogedIdUserId = this.GetUserId()
                    });
                }

            }

            return View(newsFeedHomeIndexViewModel);
        }


        private string GetUserId()
        {
            return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [Authorize]
        public IActionResult NewPost()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        [ActionName("NewPost")]
        public async Task<IActionResult> NewPost(PostInputViewModel post)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(post);
            }

            string postId = this.usersPostsService.AddPostToUser(this.User.FindFirstValue(ClaimTypes.NameIdentifier), post.Description);

            foreach (var photo in post.Photos)
            {
                var photoContent = await GetFileContent(photo);
                var photoId = await this.usersPostsService.AddPhotoToPost(postId);
                await this.SavePhotoToLocalSystem(photoId, photoContent);
            }

            foreach (var video in post.Videos)
            {
                var videoContent = await GetFileContent(video);
                var videoId = await this.usersPostsService.AddVideoToPost(postId);
                await this.SavePhotoToLocalSystem(videoId, videoContent);
            }



            return this.Redirect("/");

        }

        private async Task SavePhotoToLocalSystem(string photoId, byte[] photoContent)
        {
            var directory = this.env.WebRootPath;
            await System.IO.File.WriteAllBytesAsync(directory + @"/postsData/" + $"{photoId}.jpg", photoContent);
        }

        private static async Task<byte[]> GetFileContent(IFormFile photo)
        {
            using (var stream = new MemoryStream())
            {
                await photo.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);
                var photoContent = stream.ToArray();
                return photoContent;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

