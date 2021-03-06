﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Controllers.Extensions;
using SocialNetwork.Models.Likes;
using SocialNetwork.Services.LikesManagement;

namespace SocialNetwork.Controllers
{
    public class LikesController : Controller
    {
        private readonly ILikesService likesService;

        private readonly IControllerAdditionalFunctionality controllerAdditionalFunctionality;

        public LikesController(ILikesService likesService, IControllerAdditionalFunctionality controllerAdditionalFunctionality)
        {
            this.likesService = likesService;
            this.controllerAdditionalFunctionality = controllerAdditionalFunctionality;
        }

        [Authorize]
        public async Task<UserLikedOrUnlikedPostReturnModel> UserLikedOrUnlikedPost(string userWhoLikesIt, string likedPostId)
        {
            bool hasUsersLikedThePost, hasUserUnLikedThePost;

            if (await this.likesService.DoesUserLikePostAsync(userWhoLikesIt, likedPostId) == false)
            {
                await this.likesService.AddUserLikesPost(userWhoLikesIt, likedPostId);
                hasUsersLikedThePost = true;
                hasUserUnLikedThePost = false;
            }
            else
            {
                await this.likesService.RemoveUserDislikesPost(userWhoLikesIt, likedPostId);
                hasUsersLikedThePost = false;
                hasUserUnLikedThePost = true;
            }

            List<UserLikedPostViewModel> usersLikedThePost = this.likesService.GetPeopleWhoLikePost(likedPostId)
                .Select(userWhoLikes => new UserLikedPostViewModel(
                    userWhoLikes.Id,
                    this.GetProfilePicturePath(userWhoLikes.Id),
                    userWhoLikes.UserName))
                .ToList();

            return new UserLikedOrUnlikedPostReturnModel(
                usersLikedThePost.Count,
                usersLikedThePost,
                hasUserUnLikedThePost,
                hasUsersLikedThePost);
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

        private string GetFileUrl(string photoId)
        {
            return $"/postsData/{photoId}.jpg";
        }
    }
}