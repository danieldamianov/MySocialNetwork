using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
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

        public HomeController(
            IFollowingService usersFollowingFunctionalityService,
            IUsersPostsService usersPostsService,
            ImageConverter imageConverter,
            IControllerAdditionalFunctionality controllerAdditionalFunctionality,
            TimeConvertingService timeConvertingService,
            ILikesService likesService)
        {
            this.usersFollowingFunctionalityService = usersFollowingFunctionalityService;
            this.usersPostsService = usersPostsService;
            this.imageConverter = imageConverter;
            this.controllerAdditionalFunctionality = controllerAdditionalFunctionality;
            this.timeConvertingService = timeConvertingService;
            this.likesService = likesService;
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
            if (ModelState.IsValid == false)
            {
                return this.View(post);
            }

            using (var stream = new MemoryStream())
            {
                await post.Files[0].CopyToAsync(stream);

                stream.Seek(0, SeekOrigin.Begin);

                this.usersPostsService.AddPostToUser(this.User.FindFirstValue(ClaimTypes.NameIdentifier)
                    , stream.ToArray(), post.Description);
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

