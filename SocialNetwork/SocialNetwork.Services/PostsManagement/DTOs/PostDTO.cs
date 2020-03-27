using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Services.PostsManagement.DTOs
{
    public class PostDTO
    {
        public PostDTO(
            string postId,
            string creatorId,
            string description,
            string username,
            DateTime dateTimeCreated,
            List<string> photosIds,
            List<string> videosIds,
            List<CommentDTO> comments)
        {
            this.Description = description;
            this.PhotosIds = photosIds;
            this.VideosIds = videosIds;
            this.Username = username;
            this.DateTimeCreated = dateTimeCreated;
            this.PostId = postId;
            this.Comments = comments;
            this.CreatorId = creatorId;
        }

        public string PostId { get; set; }

        public string Username { get; set; }

        public string Description { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public List<CommentDTO> Comments { get; set; }

        public string CreatorId { get; set; }

        public List<string> PhotosIds { get; }

        public List<string> VideosIds { get; }
    }
}
