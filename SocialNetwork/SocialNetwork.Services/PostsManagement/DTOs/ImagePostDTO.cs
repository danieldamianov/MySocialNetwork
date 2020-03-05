using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Services.PostsManagement.DTOs
{
    public class ImagePostDTO
    {
        public string PostId { get; set; }
        public string Username { get; set; }
        public ImagePostDTO(string postId,string creatorId, string description, byte[] photo,string username, DateTime dateTimeCreated
            ,List<CommentDTO> comments)
        {
            this.Description = description;
            this.Photo = photo;
            this.Username = username;
            this.DateTimeCreated = dateTimeCreated;
            this.PostId = postId;
            this.Comments = comments;
            this.CreatorId = creatorId;
        }

        public string Description { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public byte[] Photo { get; set; }

        public List<CommentDTO> Comments { get; set; }

        public string CreatorId { get; set; }
    }
}
