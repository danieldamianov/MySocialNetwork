using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SocialNetwork.DatabaseModels
{
    public class Reply
    {
        public Reply(string content, string creatorId, string commentId)
        {
            Id = Guid.NewGuid().ToString();
            Content = content;
            CreatorId = creatorId;
            CommentId = commentId;
        }

        [Key]
        public string Id { get; set; }

        [MaxLength(200)]
        public string Content { get; set; }

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public string CommentId { get; set; }

        public Comment Comment { get; set; }
    }
}
