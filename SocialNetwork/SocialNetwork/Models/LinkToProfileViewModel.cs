using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class LinkToProfileViewModel
    {
        public LinkToProfileViewModel(string userId, string avatarCode, string username, int avatarSize)
        {
            this.UserId = userId;
            this.ProfilePicturePath = avatarCode;
            this.Username = username;
            this.ProfilePictureSize = avatarSize;
        }

        public string UserId { get; set; }

        public string ProfilePicturePath { get; set; }

        public string Username { get; set; }

        public int ProfilePictureSize { get; set; }
    }
}
