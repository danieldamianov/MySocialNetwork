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
            User user = this.socialNetworkContext.Users.Include(u => u.Followed)
                .Single(u => u.Id == userId);

            return user.Followed.Select(followed => followed.FollowedId).ToList();
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


        public void AddUser(string userId, string name)
        {
            if (this.socialNetworkContext.Users.Find(userId) == null)
            {
                this.socialNetworkContext.Users.Add(new User()
                {
                    Id = userId,
                    Name = name
                });

                this.socialNetworkContext.SaveChanges();
            }

        }

        public UserWithFollowersAndFollowing GetUserById(string id)
        {
            User user = socialNetworkContext.Users.Find(id);

            return new UserWithFollowersAndFollowing()
            {
                Id = user.Id,
                Name = user.Name
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
