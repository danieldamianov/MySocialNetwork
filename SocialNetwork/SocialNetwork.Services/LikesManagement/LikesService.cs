using SocialNetwork.Data;
using SocialNetwork.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Services.LikesManagement
{
    public class LikesService : ILikesService
    {
        private readonly SocialNetworkDbContext socialNetworkDbContext;

        public LikesService(SocialNetworkDbContext socialNetworkDbContext)
        {
            this.socialNetworkDbContext = socialNetworkDbContext;
        }
        public async Task<bool> AddUserLikesPost(string userId, string postId)
        {
            if (this.socialNetworkDbContext.UsersLikedPosts.Find(userId,postId) != null)
            {
                return false;
            }
            await this.socialNetworkDbContext.UsersLikedPosts.AddAsync(new UsersLikedPosts(userId, postId));
            await this.socialNetworkDbContext.SaveChangesAsync();
            return true;
        }

        public List<string> GetPeopleWhoLikePost(string postId)
        {
            return this.socialNetworkDbContext.UsersLikedPosts
                .Where(userLikedPost => userLikedPost.PostId == postId)
                .Select(userLikedPost => userLikedPost.User.UserName)
                .ToList();
        }
    }
}
