using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.DatabaseModels
{
    public class UsersLikedPosts
    {
        public UsersLikedPosts(string userId, string postId)
        {
            this.UserId = userId;
            this.PostId = postId;
        }

        public string UserId { get; set; }

        public string PostId { get; set; }

        public SocialNetworkUser User { get; set; }

        public Post Post { get; set; }
    }
}
