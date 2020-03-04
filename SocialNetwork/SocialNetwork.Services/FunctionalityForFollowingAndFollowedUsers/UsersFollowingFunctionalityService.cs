using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialNetwork.Services.FunctionalityForFollowingAndFollowedUsers.DbTransferObjects;
using SocialNetwork.DatabaseModels;

namespace SocialNetwork.Services.FunctionalityForFollowingAndFollowedUsers
{
    public class UsersFollowingFunctionalityService
    {
        public SocialNetworkDbContext socialNetworkContext { get; set; }

        public UsersFollowingFunctionalityService(SocialNetworkDbContext socialNetworkContext)
        {
            this.socialNetworkContext = socialNetworkContext;
        }

        public List<string> GetUsersIdsWhichGivenUserFollows(string userId)
        {
            SocialNetworkUser user = this.socialNetworkContext.Users.Include(u => u.Followed)
                .Single(u => u.Id == userId);

            return user.Followed.Select(followed => followed.FollowedId).ToList();
        }

        public List<UserWithFollowersAndFollowing> GetUserByFirstLetters(string firstLetters)
        {
            return socialNetworkContext.Users.Where(user => user.UserName.StartsWith(firstLetters))
                .ToList()
                .Select(user => new UserWithFollowersAndFollowing()
                {
                    Id = user.Id,
                    Name = user.UserName,
                })
                .ToList();

        }

        public UserWithFollowersAndFollowing GetUserById(string id)
        {
            SocialNetworkUser user = socialNetworkContext.Users.Find(id);

            return new UserWithFollowersAndFollowing()
            {
                Id = user.Id,
                Name = user.UserName
            };

        }

        public bool AddFollowingRelationShip(string followerId, string followedId)
        {
            if (this.socialNetworkContext.FollowersFollowed.Find(followerId,followedId) != null)
            {
                return false;
            }

            this.socialNetworkContext.FollowersFollowed.Add(new FollowerFollowed(followerId, followedId));
            this.socialNetworkContext.SaveChanges();

            return true;
        }
    }
}
