﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SocialNetwork.DatabaseModels
{
    public class User
    {
        public User()
        {
            Followed = new List<FollowerFollowed>();
            Followers = new List<FollowerFollowed>();
            Posts = new List<ImagePost>();
            Replies = new List<Reply>();
            Comments = new List<Comment>();
        }

        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public byte[] Photo { get; set; }

        public List<FollowerFollowed> Followed { get; set; } // users that the users follows

        public List<FollowerFollowed> Followers { get; set; } // users that follow the user

        public List<ImagePost> Posts { get; set; }

        public List<Reply> Replies { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
