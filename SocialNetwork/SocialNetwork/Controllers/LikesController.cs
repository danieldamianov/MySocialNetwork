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
        public async Task<bool> LikePost(string likedPostId,string userWhoLikesIt)
        {
            return await this.likesService.AddUserLikesPost(userWhoLikesIt, likedPostId);
        }
    }
}