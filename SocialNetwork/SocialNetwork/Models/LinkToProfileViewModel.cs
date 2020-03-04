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
            UserId = userId;
            AvatarCode = avatarCode;
            Username = username;
            AvatarSize = avatarSize;
        }

        public string UserId { get; set; }

        public string AvatarCode { get; set; }

        public string Username { get; set; }

        public int AvatarSize { get; set; }
    }
}
