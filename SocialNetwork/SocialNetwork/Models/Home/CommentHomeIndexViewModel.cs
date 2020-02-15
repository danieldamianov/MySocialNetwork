using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.Home
{
    public class CommentHomeIndexViewModel
    {
        public CommentHomeIndexViewModel(string content, string username,string userId, string userAvatar)
        {
            this.Content = content;
            this.Username = username;
            this.UserId = userId;
            this.UserAvatar = userAvatar;
        }

        public string CommentId { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public string UserId { get; set; }

        public string UserAvatar { get; set; }
    }
}
