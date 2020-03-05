using System.Collections.Generic;

namespace SocialNetwork.Services.FollowingManagement.DTOs
{
    public class UserWithFollowersAndFollowingDTO
    {
        public UserWithFollowersAndFollowingDTO()
        {
            this.Followed = new List<UserWithFollowersAndFollowingDTO>();
            this.Followers = new List<UserWithFollowersAndFollowingDTO>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public List<UserWithFollowersAndFollowingDTO> Followed { get; set; } // users that the users follows

        public List<UserWithFollowersAndFollowingDTO> Followers { get; set; } // users that follow the user
    }
}
