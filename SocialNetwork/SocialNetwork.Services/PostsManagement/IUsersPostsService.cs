using System.Collections.Generic;

using SocialNetwork.Services.PostsManagement.DTOs;

namespace SocialNetwork.Services.PostsManagement
{
    public interface IUsersPostsService
    {
        void AddPostToUser(string userId, byte[] photo, string description);

        List<ImagePostDTO> GetAllImagePostsOfGivenUsersIds(List<string> userIds);
    }
}
