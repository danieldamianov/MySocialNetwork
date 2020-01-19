using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.DatabaseModels;
using SocialNetwork.Services.DatabaseTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork.Services
{
    public class UsersFollowingFunctionalityService
    {
        public SocialNetworkDbContext socialNetworkContext { get; set; }

        public UsersFollowingFunctionalityService(SocialNetworkDbContext socialNetworkContext)
        {
            this.socialNetworkContext = socialNetworkContext;
        }

        public List<UserWithFollowersAndFollowing> GetUserByFirstLetters(string firstLetters)
        {
            return socialNetworkContext.Users.Where(user => user.Name.StartsWith(firstLetters))
                .ToList()
                .Select(user => new UserWithFollowersAndFollowing()
                {
                    Id = user.Id,
                    Name = user.Name,
                })
                .ToList();

        }

    }
}
