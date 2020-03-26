using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DatabaseModels
{
    public class Comment
    {
        public Comment(string content, string creatorId, string postId, DateTime createdOn)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Content = content;
            this.CreatorId = creatorId;
            this.PostId = postId;
            this.CreatedOn = createdOn;
        }

        [Key]
        public string Id { get; set; }

        [MaxLength(200)]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatorId { get; set; }

        public SocialNetworkUser Creator { get; set; }

        public string PostId { get; set; }

        public Post Post { get; set; }

        public List<Reply> Replies { get; set; }
    }
}
