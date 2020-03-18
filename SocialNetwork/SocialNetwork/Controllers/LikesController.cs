using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Services.LikesManagement;

namespace SocialNetwork.Controllers
{
    public class LikesController : Controller
    {
        private readonly ILikesService likesService;

        public LikesController(ILikesService likesService)
        {
            this.likesService = likesService;
        }

        [Authorize]
        public async Task<object> UserLikedOrUnlikedPost(string userWhoLikesIt ,string likedPostId)
        {
            if (await this.likesService.DoesUserLikePost(userWhoLikesIt, likedPostId) == false)
            {
                await this.likesService.AddUserLikesPost(userWhoLikesIt, likedPostId);
                return new {usersHasLikedThePost = true, usersHasUnLikedThePost = false, };
            }
            else
            {
                await this.likesService.RemoveUserDislikesPost(userWhoLikesIt, likedPostId);
                return new { usersHasLikedThePost = false, usersHasUnLikedThePost = true, };
            }
        }
    }
}