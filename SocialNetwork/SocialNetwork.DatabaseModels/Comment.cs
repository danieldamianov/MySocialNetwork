using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DatabaseModels
{
    public class Comment
    {
        public Comment(string content, string creatorId, string postId)
        {
            Id = Guid.NewGuid().ToString();
            Content = content;
            CreatorId = creatorId;
            PostId = postId;
        }

        [Key]
        public string Id { get; set; }

        [MaxLength(200)]
        public string Content { get; set; }

        public string CreatorId { get; set; }

        public SocialNetworkUser Creator { get; set; }

        public string PostId { get; set; }

        public ImagePost Post { get; set; }

        public List<Reply> Replies { get; set; }
    }
}
