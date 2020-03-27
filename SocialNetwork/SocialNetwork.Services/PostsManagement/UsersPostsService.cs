using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.DatabaseModels;
using SocialNetwork.Services.PostsManagement.DTOs;

namespace SocialNetwork.Services.PostsManagement
{
    public class UsersPostsService : IUsersPostsService
    {
        private readonly SocialNetworkDbContext socialNetworkDbContext;

        public UsersPostsService(SocialNetworkDbContext socialNetworkDbContext)
        {
            this.socialNetworkDbContext = socialNetworkDbContext;
        }

        public async Task<string> AddPhotoToPost(string postId)
        {
            ImageContent image = new ImageContent(postId);
            var post = await this.socialNetworkDbContext.Posts.FindAsync(postId);
            post.Images.Add(image);
            await this.socialNetworkDbContext.SaveChangesAsync();
            return image.Id;
        }

        public string AddPostToUser(string userId, string description)
        {
            Post post = new Post(description, userId);
            this.socialNetworkDbContext.Users.Find(userId)
                .Posts.Add(post);

            this.socialNetworkDbContext.SaveChanges();

            return post.Id;
        }

        public async Task<string> AddVideoToPost(string postId)
        {
            VideoContent video = new VideoContent(postId);
            var post = await this.socialNetworkDbContext.Posts.FindAsync(postId);
            post.Videos.Add(video);
            await this.socialNetworkDbContext.SaveChangesAsync();
            return video.Id;
        }

        public List<PostDTO> GetAllImagePostsOfGivenUsersIds(List<string> userIds)
        {
            return this.socialNetworkDbContext.Posts
                .Include(post => post.Creator)
                .Include(post => post.Comments)
                .Include(post => post.Images)
                .Include(post => post.Videos)
                .Where(post => userIds.Contains(post.CreatorId)) 
                .Select(post => new PostDTO(
                    post.Id,
                    post.CreatorId,
                    post.Description,
                    post.Creator.UserName,
                    post.DateTimeCreated,
                    post.Images.Select(image => image.Id).ToList(),
                    post.Videos.Select(video => video.Id).ToList(),
                    post.Comments.Select(comment => new CommentDTO(
                        comment.Creator.UserName,
                        comment.Content,
                        comment.Creator.Id))
                    .ToList()))
                .ToList();
        }
    }
}
