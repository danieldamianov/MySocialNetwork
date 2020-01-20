﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SocialNetwork.DatabaseModels
{
    public class ImagePost
    {
        public ImagePost(byte[] photo, string description, string creatorId)
        {
            Id = Guid.NewGuid().ToString();
            Photo = photo;
            Description = description;
            CreatorId = creatorId;
        }

        [Key]
        public string Id { get; set; }

        public byte[] Photo { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public List<Comment> Comments { get; set; }
    }
}