using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Services.FunctionalityForMangementOfComments;

namespace SocialNetwork.Controllers
{
    public class CommentsController : Controller
    {
        private readonly CommentsFunctionalityService commentsFunctionalityService;

        public CommentsController(CommentsFunctionalityService commentsFunctionalityService)
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