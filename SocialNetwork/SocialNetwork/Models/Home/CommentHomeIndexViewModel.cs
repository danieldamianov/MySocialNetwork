using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.Home
{
    public class CommentHomeIndexViewModel
    {
        public CommentHomeIndexViewModel(string content, string username)
        {
            Content = content;
            Username = username;
        }

        public string CommentId { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }
    }
}
