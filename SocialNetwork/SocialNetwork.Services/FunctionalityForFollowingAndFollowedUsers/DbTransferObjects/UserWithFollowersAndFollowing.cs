using System.Collections.Generic;

namespace SocialNetwork.Services.FunctionalityForFollowingAndFollowedUsers.DbTransferObjects
{
    public class UserWithFollowersAndFollowing
    {
        public UserWithFollowersAndFollowing()
        {
            this.Followed = new List<UserWithFollowersAndFollowing>();
            this.Followers = new List<UserWithFollowersAndFollowing>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public List<UserWithFollowersAndFollowing> Followed { get; set; } // users that the users follows

        public List<UserWithFollowersAndFollowing> Followers { get; set; } // users that follow the user
    }
}
