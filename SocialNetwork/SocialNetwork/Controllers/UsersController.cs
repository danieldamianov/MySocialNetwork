using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork.Data;
using SocialNetwork.Models.Users;
using SocialNetwork.Services;
using SocialNetwork.Services.DatabaseTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;

        public UsersFollowingFunctionalityService UsersFollowingFunctionalityService;

        public ApplicationDbContext ApplicationDbContext;

        public UsersController(ILogger<UsersController> logger, UsersFollowingFunctionalityService usersFollowingFunctionalityService
            ,ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            UsersFollowingFunctionalityService = usersFollowingFunctionalityService;
        }


        public IActionResult Search(string search)
        {
            List<UserWithFollowersAndFollowing> users = this.UsersFollowingFunctionalityService.GetUserByFirstLetters(search);
            UsersSearchViewModel usersSearchViewModel = new UsersSearchViewModel()
            {
                Usernames = users.Select(user => user.Name).ToList()
            };
            return View(usersSearchViewModel);
        }
    }
}
