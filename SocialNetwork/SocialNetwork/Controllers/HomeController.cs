﻿using System;
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

        private readonly IControllerAdditionalFunctionality controllerAdditionalFunctionality;

        private readonly TimeConvertingService timeConvertingService;

        private readonly ILikesService likesService;

        private readonly IWebHostEnvironment env;

        public HomeController(
            IFollowingService usersFollowingFunctionalityService,
            IUsersPostsService usersPostsService,
            IControllerAdditionalFunctionality controllerAdditionalFunctionality,
            TimeConvertingService timeConvertingService,
            ILikesService likesService,
            IWebHostEnvironment env)
        {
            this.usersFollowingFunctionalityService = usersFollowingFunctionalityService;
            this.usersPostsService = usersPostsService;
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

                List<PostDTO> imagePostsOfFollowingUsers =
                    this.usersPostsService.GetAllImagePostsOfGivenUsersIds(this.usersFollowingFunctionalityService
                    .GetUsersIdsWhichGivenUserFollows(this.GetUserId()))
                    .OrderByDescending(p => p.DateTimeCreated)
                    .ToList();


                foreach (var post in imagePostsOfFollowingUsers)
                {
                    List<UserLikedPostHomeIndexViewModel> usersWhoLikeTheCurrentPost =
                        this.likesService.GetPeopleWhoLikePost(post.PostId).Select(
                            user => new UserLikedPostHomeIndexViewModel(
                                user.UserName,
                                user.Id,
                                this.GetProfilePicturePath(user.Id)))
                        .ToList();

                    newsFeedHomeIndexViewModel.Posts.Add(new PostHomeIndexViewModel
                    {
                        Description = post.Description,
                        Username = post.Username,
                        TimeSinceCreated = this.timeConvertingService.ConvertDateTime(post.DateTimeCreated),
                        PostId = post.PostId,
                        Comments = post.Comments.Select(comment => new CommentHomeIndexViewModel(
                            comment.Content,
                            comment.Username,
                            comment.UserId,
                            this.GetProfilePicturePath(comment.UserId))).ToList(),
                        UserProfilePicturePath = this.GetProfilePicturePath(post.CreatorId),
                        UserId = post.CreatorId,
                        UsersLikedThePost = usersWhoLikeTheCurrentPost,
                        HasCurrentUserLikedThePost = usersWhoLikeTheCurrentPost.Any(user => user.Id == this.GetUserId()),
                        LogedIdUserId = this.GetUserId(),
                        PhotosPaths = post.PhotosIds.Select(photoId => this.GetFileUrl(photoId)).ToList(),
                        VideosPaths = post.VideosIds.Select(videoId => this.GetFileUrl(videoId)).ToList(),
                    });
                }
            }

            return this.View(newsFeedHomeIndexViewModel);
        }

        private string GetFileUrl(string photoId)
        {
            return $"/postsData/{photoId}.jpg";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
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
                var photoContent = await this.GetFileContent(photo);
                var photoId = await this.usersPostsService.AddPhotoToPost(postId);
                await this.SavePhotoToLocalSystemAsync(photoId, photoContent);
            }

            foreach (var video in post.Videos)
            {
                var videoContent = await this.GetFileContent(video);
                var videoId = await this.usersPostsService.AddVideoToPost(postId);
                await this.SavePhotoToLocalSystemAsync(videoId, videoContent);
            }

            return this.Redirect("/");
        }

        private async Task SavePhotoToLocalSystemAsync(string fileId, byte[] photoContent)
        {
            var directory = this.env.WebRootPath;
            await System.IO.File.WriteAllBytesAsync(directory + @"/postsData/" + $"{fileId}.jpg", photoContent);
        }

        private async Task<byte[]> GetFileContent(IFormFile photo)
        {
            using (var stream = new MemoryStream())
            {
                await photo.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);
                var photoContent = stream.ToArray();
                return photoContent;
            }
        }

        private string GetUserId()
        {
            return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [NonAction]
        private string GetProfilePicturePath(string userId)
        {
            string profilePictureId = this.controllerAdditionalFunctionality.GetProfilePictureId(userId);

            string profilePicturePath = string.Empty;

            if (profilePictureId == null)
            {
                profilePicturePath = "/pics/user_def_pic.png";
            }
            else
            {
                profilePicturePath = this.GetFileUrl(profilePictureId);
            }

            return profilePicturePath;
        }
    }
}