using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.DatabaseModels;
using SocialNetwork.Services.PostsManagement.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public List<ImagePostDTO> GetAllImagePostsOfGivenUsersIds(List<string> userIds)
        {
            return this.socialNetworkDbContext.Posts
                .Include(imagePost => imagePost.Creator)
                .Include(imagePost => imagePost.Comments)
                .Where(imagePost => userIds.Contains(imagePost.CreatorId))// TODO: Refactor afterwards
                .Select(imagePost => new ImagePostDTO(imagePost.Id, imagePost.CreatorId, imagePost.Description, null, imagePost.Creator.UserName,
                imagePost.DateTimeCreated,
                imagePost.Comments.Select(comment => new CommentDTO(comment.Creator.UserName, comment.Content,
                comment.Creator.Id
                )).ToList()))
                .ToList();
        }
    }
}
