using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SocialNetwork.DatabaseModels
{
    public class Post
    {
        public Post(string description, string creatorId)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Description = description;
            this.CreatorId = creatorId;
            this.DateTimeCreated = DateTime.UtcNow;
            this.Videos = new List<VideoContent>();
            this.Images = new List<ImageContent>();
        }

        [Key]
        public string Id { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string CreatorId { get; set; }

        public SocialNetworkUser Creator { get; set; }

        public List<Comment> Comments { get; set; }

        public List<UsersLikedPosts> UsersLikedThePost { get; set; }

        public List<ImageContent> Images { get; set; }

        public List<VideoContent> Videos { get; set; }
    }
}
