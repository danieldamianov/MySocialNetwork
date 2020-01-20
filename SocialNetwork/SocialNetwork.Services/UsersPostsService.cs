using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.DatabaseModels;
using SocialNetwork.Services.DatabaseTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork.Services
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
                .Posts.Add(new DatabaseModels.ImagePost(photo, description, userId));

            this.socialNetworkDbContext.SaveChanges();
        }

        public List<string> GetUsersIdsWhichGivenUserFollows(string userId)
        {
            User user = this.socialNetworkDbContext.Users.Include(u => u.Followed)
                .Single(u => u.Id == userId);

            return user.Followed.Select(followed => followed.FollowedId).ToList();
        }

        public List<ImagePostDTO> GetAllImagePostsOfGivenUsersIds(List<string> userIds)
        {
            return this.socialNetworkDbContext.ImagePosts
                .Where(imagePost => userIds.Contains(imagePost.CreatorId))
                .Select(imagePost => new ImagePostDTO(imagePost.Description, imagePost.Photo))
                .ToList();
        }
    }
}
