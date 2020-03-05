using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.DatabaseModels;
using SocialNetwork.Services.PostsManagement.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Services.PostsManagement
{
    public interface IUsersPostsService
    {
        void AddPostToUser(string userId, byte[] photo, string description);

        List<ImagePostDTO> GetAllImagePostsOfGivenUsersIds(List<string> userIds);
    }
}
