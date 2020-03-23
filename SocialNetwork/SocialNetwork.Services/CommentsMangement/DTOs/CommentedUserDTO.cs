using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Services.CommentsMangement.DTOs
{
    public class CommentedUserDTO
    {
        public CommentedUserDTO(string userId, string username, byte[] avatar,string comment)
        {
            this.UserId = userId;
            this.Username = username;
            this.Avatar = avatar;
            this.Comment = comment;
        }

        public string UserId { get; set; }

        public string Username { get; set; }

        public byte[] Avatar { get; set; }

        public string Comment { get; set; }
    }
}
