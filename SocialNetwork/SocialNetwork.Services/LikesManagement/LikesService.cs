using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SocialNetwork.Data;
using SocialNetwork.DatabaseModels;
using SocialNetwork.Services.LikesManagement.DTOs;

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
            if (await this.DoesUserLikePostAsync(userId, postId))
            {
                return false;
            }
            await this.socialNetworkDbContext.UsersLikedPosts.AddAsync(new UsersLikedPosts(userId, postId));
            await this.socialNetworkDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DoesUserLikePostAsync(string userId, string postId)
        {
            return await this.socialNetworkDbContext.UsersLikedPosts.FindAsync(userId, postId) != null;
        }

        public List<UserWhoLikesAPostDTO> GetPeopleWhoLikePost(string postId)
        {
            return this.socialNetworkDbContext.UsersLikedPosts
                .Where(userLikedPost => userLikedPost.PostId == postId)
                .Select(userLikedPost => new UserWhoLikesAPostDTO(
                userLikedPost.User.UserName,
                userLikedPost.UserId,
                userLikedPost.User.Photo))
                .ToList();
        }

        public async Task<bool> RemoveUserDislikesPost(string userId, string postId)
        {
            if (await DoesUserLikePostAsync(userId, postId) == false)
            {
                return false;
            }

            this.socialNetworkDbContext.UsersLikedPosts.Remove(await this.socialNetworkDbContext.UsersLikedPosts
                .FindAsync(userId, postId));

            await this.socialNetworkDbContext.SaveChangesAsync();
            return true;
        }
    }
}
