using System;
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
            DateTimeCreated = DateTime.UtcNow;
        }

        [Key]
        public string Id { get; set; }

        public byte[] Photo { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string CreatorId { get; set; }

        public SocialNetworkUser Creator { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
