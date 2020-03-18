using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Services.LikesManagement.DTOs
{
    public class UserWhoLikesAPostDTO
    {
        public UserWhoLikesAPostDTO(string userName, string id, byte[] avatar)
        {
            UserName = userName;
            Id = id;
            Avatar = avatar;
        }

        public string UserName { get; set; }

        public string Id { get; set; }

        public byte[] Avatar { get; set; }
    }
}
