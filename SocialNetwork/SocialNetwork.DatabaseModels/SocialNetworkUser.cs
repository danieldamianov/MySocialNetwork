using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

using Microsoft.AspNetCore.Identity;

namespace SocialNetwork.DatabaseModels
{
    public class SocialNetworkUser : IdentityUser
    {
        public SocialNetworkUser()
        {
            this.Followed = new List<FollowerFollowed>();
            this.Followers = new List<FollowerFollowed>();
            this.Posts = new List<Post>();
            this.Replies = new List<Reply>();
            this.Comments = new List<Comment>();
        }

        public byte[] Photo { get; set; }

        public List<FollowerFollowed> Followed { get; set; } // users that the users follows

        public List<FollowerFollowed> Followers { get; set; } // users that follow the user

        public List<Post> Posts { get; set; }

        public List<Reply> Replies { get; set; }

        public List<Comment> Comments { get; set; }

        public List<UsersLikedPosts> LikedPosts { get; set; }
    }
}
