using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.DatabaseModels;
using SocialNetwork.Services.PostsManagement.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Services.PostsManagement
{
    public class UsersPostsService : IUsersPostsService
    {
        public UsersPostsService(SocialNetworkDbContext socialNetworkDbContext)
        {
            this.socialNetworkDbContext = socialNetworkDbContext;
        }

        private readonly SocialNetworkDbContext socialNetworkDbContext;

        public void AddPostToUser(string userId, byte[] photo, string description)
        {
            this.socialNetworkDbContext.Users.Find(userId)
                .Posts.Add(new Post(description, userId));

            // TODO: Refactor afterwards
            this.socialNetworkDbContext.SaveChanges();
        }

  

        public List<ImagePostDTO> GetAllImagePostsOfGivenUsersIds(List<string> userIds)
        {
            return this.socialNetworkDbContext.Posts
                .Include(imagePost => imagePost.Creator)
                .Include(imagePost => imagePost.Comments)
                .Where(imagePost => userIds.Contains(imagePost.CreatorId))// TODO: Refactor afterwards
                .Select(imagePost => new ImagePostDTO(imagePost.Id,imagePost.CreatorId,imagePost.Description, null,imagePost.Creator.UserName,
                imagePost.DateTimeCreated,
                imagePost.Comments.Select(comment => new CommentDTO(comment.Creator.UserName, comment.Content,
                comment.Creator.Id
                )).ToList()))
                .ToList();
        }
    }
}
