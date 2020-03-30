using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.Likes
{
    public class UserLikedPostViewModel
    {
        public UserLikedPostViewModel(string userId, string avatarCode, string username)
        {
            this.UserId = userId;
            this.ProfilePicturePath = avatarCode;
            this.Username = username;
        }

        public string UserId { get; set; }

        public string ProfilePicturePath { get; set; }

        public string Username { get; set; }
    }
}
