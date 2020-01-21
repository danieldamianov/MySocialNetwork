using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork.Models.Users.Profile;
using SocialNetwork.Models.Users.Search;
using SocialNetwork.Services.FunctionalityForFollowingAndFollowedUsers;
using SocialNetwork.Services.FunctionalityForFollowingAndFollowedUsers.DbTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;

        public UsersFollowingFunctionalityService UsersFollowingFunctionalityService;

        public UsersController(ILogger<UsersController> logger, UsersFollowingFunctionalityService usersFollowingFunctionalityService)
        {
            _logger = logger;
            UsersFollowingFunctionalityService = usersFollowingFunctionalityService;
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
            return this.View(new UserProfileViewModel()
            {
                Name = user.Name,
                UserId = user.Id
            });
        }

        public IActionResult Follow(string followerId, string followedId)
        {
            this.UsersFollowingFunctionalityService.AddFollowingRelationShip(followerId,followedId);
            return this.Redirect("/");
        }
    }

}

