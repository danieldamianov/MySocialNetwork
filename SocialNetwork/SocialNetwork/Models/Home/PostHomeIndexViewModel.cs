using System;

namespace SocialNetwork.Models.Home
{
    public class PostHomeIndexViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }

        public string Description { get; set; }

        public DateTime DateTimeCreated { get; set; }
        public string Code { get; set; }
    }
}