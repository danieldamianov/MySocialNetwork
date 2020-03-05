using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Services.CommentsManagement;

namespace SocialNetwork.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsFunctionalityService commentsFunctionalityService;

        public CommentsController(ICommentsFunctionalityService commentsFunctionalityService)
        {
            this.commentsFunctionalityService = commentsFunctionalityService;
        }

        [Authorize]
        public IActionResult NewComment(string creatorId, string postId, string comment)
        {
            this.commentsFunctionalityService.AddCommentToPost(creatorId, postId, comment);
            return this.Redirect("/");
        }
    }
}