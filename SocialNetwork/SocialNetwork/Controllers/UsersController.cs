using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork.Controllers.ImageConvertingFunctionality;
using SocialNetwork.Models.Users.Profile;
using SocialNetwork.Models.Users.Search;
using SocialNetwork.Services.FuctionalityForManagementOfPosts;
using SocialNetwork.Services.FuctionalityForManagementOfPosts.DbTransferObjects;
using SocialNetwork.Services.FunctionalityForFollowingAndFollowedUsers;
using SocialNetwork.Services.FunctionalityForFollowingAndFollowedUsers.DbTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;

        private readonly UsersFollowingFunctionalityService UsersFollowingFunctionalityService;

        private readonly UsersPostsService UsersPostsService;

        private readonly ImageConverter imageConverter;

        public UsersController(ILogger<UsersController> logger, 
            UsersFollowingFunctionalityService usersFollowingFunctionalityService,
            UsersPostsService usersPostsService,
            ImageConverter imageConverter)
        {
            _logger = logger;
            UsersFollowingFunctionalityService = usersFollowingFunctionalityService;
            UsersPostsService = usersPostsService;
            this.imageConverter = imageConverter;
        }


        public IActionResult Search(string search)
        {
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
                    Code = this.imageConverter.ConvertByteArratToString(post.Photo),
                    Description = post.Description
                }).ToList()
            };
        }

        public IActionResult Follow(string followerId, string followedId)
        {
            this.UsersFollowingFunctionalityService.AddFollowingRelationShip(followerId,followedId);
            return this.Redirect("/");
        }
    }

}

