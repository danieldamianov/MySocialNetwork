using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.DatabaseModels;
using SocialNetwork.Services.FuctionalityForManagementOfPosts.DbTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Services.FuctionalityForManagementOfPosts
{
    public class UsersPostsService
    {
        public UsersPostsService(SocialNetworkDbContext socialNetworkDbContext)
        {
            this.socialNetworkDbContext = socialNetworkDbContext;
        }

        private readonly SocialNetworkDbContext socialNetworkDbContext;

        public void AddPostToUser(string userId, byte[] photo, string description)
        {
            this.socialNetworkDbContext.Users.Find(userId)
                .Posts.Add(new ImagePost(photo, description, userId));

            this.socialNetworkDbContext.SaveChanges();
        }

  

        public List<ImagePostDTO> GetAllImagePostsOfGivenUsersIds(List<string> userIds)
        {
            return this.socialNetworkDbContext.ImagePosts
                .Include(imagePost => imagePost.Creator)
                .Include(imagePost => imagePost.Comments)
                .Where(imagePost => userIds.Contains(imagePost.CreatorId))
                .Select(imagePost => new ImagePostDTO(imagePost.Id,imagePost.CreatorId,imagePost.Description, imagePost.Photo,imagePost.Creator.UserName,
                imagePost.DateTimeCreated,imagePost.Comments.Select(comment => new CommentDTO(comment.Creator.UserName,comment.Content,
                comment.Creator.Id)).ToList()))
                .ToList();
        }
    }
}
