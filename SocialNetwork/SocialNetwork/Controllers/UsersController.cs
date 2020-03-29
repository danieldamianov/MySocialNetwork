using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Controllers.Extensions;
using SocialNetwork.Controllers.TimeSinceCreationFunctionality;
using SocialNetwork.Models.Home;
using SocialNetwork.Models.Users.Profile;
using SocialNetwork.Models.Users.Search;
using SocialNetwork.Services.FollowingManagement;
using SocialNetwork.Services.FollowingManagement.DTOs;
using SocialNetwork.Services.LikesManagement;
using SocialNetwork.Services.PostsManagement;
using SocialNetwork.Services.PostsManagement.DTOs;

namespace SocialNetwork.Controllers
{
    public class UsersController : Controller
    {
        private readonly IFollowingService UsersFollowingFunctionalityService;

        private readonly IUsersPostsService UsersPostsService;

        private readonly IControllerAdditionalFunctionality controllerAdditionalFunctionality;

        private readonly TimeConvertingService timeConvertingService;

        private readonly ILikesService likesService;

        public UsersController(
            IFollowingService usersFollowingFunctionalityService,
            IUsersPostsService usersPostsService,
            IControllerAdditionalFunctionality controllerAdditionalFunctionality,
            TimeConvertingService timeConvertingService,
            ILikesService likesService)
        {
            this.UsersFollowingFunctionalityService = usersFollowingFunctionalityService;
            this.UsersPostsService = usersPostsService;
            this.controllerAdditionalFunctionality = controllerAdditionalFunctionality;
            this.timeConvertingService = timeConvertingService;
            this.likesService = likesService;
        }

        [Authorize]
        public IActionResult Follow(string followerId, string followedId)
        {
            this.UsersFollowingFunctionalityService.AddFollowingRelationShip(followerId, followedId);
            return this.Redirect("/");
        }

        public IActionResult Search(string search)
        {
            List<UserWithFollowersAndFollowingDTO> users = this.UsersFollowingFunctionalityService.GetUserByFirstLetters(search);
            UsersCollectionSearchViewModel usersSearchViewModel = new UsersCollectionSearchViewModel()
            {
                Users = users.Select(user => new UserSearchViewModel()
                { Id = user.Id, Name = user.Name, Photo = this.controllerAdditionalFunctionality.GetProfilePictureId(user.Id) })
                    .ToList()
            };
            return View(usersSearchViewModel);
        }

        public IActionResult Profile(string userId)
        {
            UserWithFollowersAndFollowingDTO user = this.UsersFollowingFunctionalityService.GetUserById(userId);
            List<PostDTO> postsOfUser = this.UsersPostsService.GetAllImagePostsOfGivenUsersIds(new List<string>() { userId })
                .OrderByDescending(post => post.DateTimeCreated)
                .ToList();

            return this.View(this.FillUserProfileViewModelWithData(user, postsOfUser));
        }

        private UserProfileViewModel FillUserProfileViewModelWithData(UserWithFollowersAndFollowingDTO user, List<PostDTO> postsOfUser)
        {
            return new UserProfileViewModel()
            {
                Name = user.Name,
                UserId = user.Id,
                UserPosts = postsOfUser.Select(post =>
                {
                    List<UserLikedPostHomeIndexViewModel> usersWhoLikeTheCurrentPost =
                        this.likesService.GetPeopleWhoLikePost(post.PostId).Select(
                            user => new UserLikedPostHomeIndexViewModel(
                                user.UserName,
                                user.Id,
                                this.controllerAdditionalFunctionality.GetProfilePictureId(user.Id)))
                        .ToList();

                    return new PostHomeIndexViewModel
                    {
                        Description = post.Description,
                        Username = post.Username,
                        TimeSinceCreated = this.timeConvertingService.ConvertDateTime(post.DateTimeCreated),
                        PostId = post.PostId,
                        Comments = post.Comments.Select(comment => new CommentHomeIndexViewModel(
                            comment.Content,
                            comment.Username,
                            comment.UserId,
                            this.controllerAdditionalFunctionality.GetProfilePictureId(comment.UserId))).ToList(),
                        UserProfilePicturePath = this.controllerAdditionalFunctionality.GetProfilePictureId(post.CreatorId),
                        UserId = post.CreatorId,
                        UsersLikedThePost = usersWhoLikeTheCurrentPost,
                        HasCurrentUserLikedThePost = usersWhoLikeTheCurrentPost.Any(user => user.Id == this.GetUserId()),
                        LogedIdUserId = this.GetUserId(),
                        PhotosPaths = post.PhotosIds.Select(photoId => this.GetFileUrl(photoId)).ToList(),
                        VideosPaths = post.VideosIds.Select(videoId => this.GetFileUrl(videoId)).ToList(),
                    };
                })
                .ToList(),
                ProfilePicturePath = this.GetProfilePicturePath(user.Id),
            };
        }

        [NonAction]
        private string GetProfilePicturePath(string userId)
        {
            string profilePictureId = this.controllerAdditionalFunctionality.GetProfilePictureId(userId);

            string profilePicturePath = string.Empty;

            if (profilePictureId == null)
            {
                profilePicturePath = "pics/user_def_pic.png";
            }
            else
            {
                profilePicturePath = this.GetFileUrl(profilePictureId);
            }

            return profilePicturePath;
        }

        [NonAction]
        private string GetUserId()
        {
            return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private string GetFileUrl(string photoId)
        {
            return $"/postsData/{photoId}.jpg";
        }
    }
}