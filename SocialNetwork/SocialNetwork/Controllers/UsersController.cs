using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork.Controllers.ImageConvertingFunctionality;
using SocialNetwork.Models.Users.Profile;
using SocialNetwork.Models.Users.Search;
using SocialNetwork.Services.FuctionalityForManagementOfPosts;
using SocialNetwork.Services.FuctionalityForManagementOfPosts.DbTransferObjects;
using SocialNetwork.Services.FunctionalityForFollowingAndFollowedUsers;
using SocialNetwork.Services.FunctionalityForFollowingAndFollowedUsers.DbTransferObjects;
using SocialNetwork.Services.FunctionalityForProfileManagement;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SocialNetwork.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;

        private readonly UsersFollowingFunctionalityService UsersFollowingFunctionalityService;

        private readonly UsersPostsService UsersPostsService;

        private readonly ImageConverter imageConverter;

        private readonly ProfileManagementService profileManagementService;

        public UsersController(ILogger<UsersController> logger,
            UsersFollowingFunctionalityService usersFollowingFunctionalityService,
            UsersPostsService usersPostsService,
            ImageConverter imageConverter,
            ProfileManagementService profileManagementService)
        {
            _logger = logger;
            this.UsersFollowingFunctionalityService = usersFollowingFunctionalityService;
            this.UsersPostsService = usersPostsService;
            this.imageConverter = imageConverter;
            this.profileManagementService = profileManagementService;
        }



        //private void SetProfileLinkData()
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

        private string GetUserId()
        {
            return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }


        public IActionResult Search(string search)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                //SetProfileLinkData(); 
            }
            List<UserWithFollowersAndFollowing> users = this.UsersFollowingFunctionalityService.GetUserByFirstLetters(search);
            UsersCollectionSearchViewModel usersSearchViewModel = new UsersCollectionSearchViewModel()
            {
                Users = users.Select(user => new UserSearchViewModel() { Id = user.Id, Name = user.Name })
                    .ToList()
            };
            return View(usersSearchViewModel);
        }

        public IActionResult Profile(string userId)
        {
            UserWithFollowersAndFollowing user = this.UsersFollowingFunctionalityService.GetUserById(userId);
            List<ImagePostDTO> postsOfUser = this.UsersPostsService.GetAllImagePostsOfGivenUsersIds(new List<string>() { userId });

            return this.View(FillUserProfileViewModelWithData(user, postsOfUser));

        }

        private UserProfileViewModel FillUserProfileViewModelWithData(UserWithFollowersAndFollowing user, List<ImagePostDTO> postsOfUser)
        {
            return new UserProfileViewModel()
            {
                Name = user.Name,
                UserId = user.Id,
                UserPosts = postsOfUser.Select(post => new PostUsersProfileViewModel()
                {
                    Code = this.imageConverter.ConvertByteArrayToString(post.Photo),
                    Description = post.Description,
                    DateTimeCreated = post.DateTimeCreated
                }).OrderByDescending(post => post.DateTimeCreated)
                .ToList(),
                Photo = GetViewedProfilePicture(user)
            };
        }

        private string GetViewedProfilePicture(UserWithFollowersAndFollowing user)
        {
            byte[] photoByteArray = this.profileManagementService.GetUserProfileLinkById(user.Id).Photo;
            string photo = string.Empty;
            if (photoByteArray != null)
            {
                photo = this.imageConverter.ConvertByteArrayToString(photoByteArray);
            }
            else
            {
                photo = imageConverter.ConvertByteArrayToString(System.IO.File.ReadAllBytes("wwwroot/pics/user_def_pic.png"));
            }
            
            return photo;
        }

        [Authorize]
        public IActionResult Follow(string followerId, string followedId)
        {
            this.UsersFollowingFunctionalityService.AddFollowingRelationShip(followerId, followedId);
            return this.Redirect("/");
        }
    }

}

