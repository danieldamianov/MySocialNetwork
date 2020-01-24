using System;
using System.Collections.Generic;

namespace SocialNetwork.Models.Home
{
    public class PostHomeIndexViewModel
    {
        public PostHomeIndexViewModel()
        {
            this.Comments = new List<CommentHomeIndexViewModel>();
        }
        public string PostId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }

        public string Description { get; set; }

        public DateTime DateTimeCreated { get; set; }
        public string Code { get; set; }

        public List<CommentHomeIndexViewModel> Comments { get; set; }
    }
}