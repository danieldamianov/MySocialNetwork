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
            Followed = new List<FollowerFollowed>();
            Followers = new List<FollowerFollowed>();
            Posts = new List<ImagePost>();
            Replies = new List<Reply>();
            Comments = new List<Comment>();
        }

        public byte[] Photo { get; set; }

        public List<FollowerFollowed> Followed { get; set; } // users that the users follows

        public List<FollowerFollowed> Followers { get; set; } // users that follow the user

        public List<ImagePost> Posts { get; set; }

        public List<Reply> Replies { get; set; }

        public List<Comment> Comments { get; set; }

        public List<UsersLikedPosts> LikedPosts { get; set; }
    }
}
