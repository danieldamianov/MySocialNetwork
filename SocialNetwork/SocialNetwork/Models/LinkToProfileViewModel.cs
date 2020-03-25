﻿using System;
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
            this.AvatarCode = avatarCode;
            this.Username = username;
            this.AvatarSize = avatarSize;
        }

        public string UserId { get; set; }

        public string AvatarCode { get; set; }

        public string Username { get; set; }

        public int AvatarSize { get; set; }
    }
}
