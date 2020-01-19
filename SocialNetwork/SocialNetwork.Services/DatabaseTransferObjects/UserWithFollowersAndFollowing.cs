using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Services.DatabaseTransferObjects
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
