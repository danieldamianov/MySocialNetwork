using System.Collections.Generic;
using System.Threading.Tasks;
using SocialNetwork.Services.PostsManagement.DTOs;

namespace SocialNetwork.Services.PostsManagement
{
    public interface IUsersPostsService
    {
        string AddPostToUser(string userId, string description);

        List<ImagePostDTO> GetAllImagePostsOfGivenUsersIds(List<string> userIds);

        Task<string> AddPhotoToPost(string postId);

        Task<string> AddVideoToPost(string postId);
    }
}
