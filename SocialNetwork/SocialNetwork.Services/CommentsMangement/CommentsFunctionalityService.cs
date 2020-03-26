using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.DatabaseModels;
using SocialNetwork.Services.CommentsMangement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Services.CommentsManagement
{
    public class CommentsFunctionalityService : ICommentsFunctionalityService
    {
        private readonly SocialNetworkDbContext socialNetworkDbContext;

        public CommentsFunctionalityService(SocialNetworkDbContext socialNetworkDbContext)
        {
            this.socialNetworkDbContext = socialNetworkDbContext;
        }


        public void AddCommentToPost(string creatorId, string postId, string content)
        {
            this.socialNetworkDbContext.Comments.Add(new DatabaseModels.Comment(content, creatorId, postId,DateTime.UtcNow));//TODO : refactor
            this.socialNetworkDbContext.SaveChanges();
        }

        public IEnumerable<CommentedUserDTO> GetAllUsersWhoHaveCommentedPost(string postId)
        {
            Post post = this.socialNetworkDbContext.Posts
                .Include(post => post.Comments)
                .ThenInclude(comment => comment.Creator)
                .First(post => post.Id == postId);

            return post.Comments
                .Select(comment => new CommentedUserDTO(
                comment.CreatorId,
                comment.Creator.UserName,
                comment.Creator.Photo,
                comment.Content));
        }
    }
}
