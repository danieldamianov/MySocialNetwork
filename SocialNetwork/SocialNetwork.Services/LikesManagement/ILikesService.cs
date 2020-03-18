using SocialNetwork.Services.LikesManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Services.LikesManagement
{

    public interface ILikesService
    {
        Task<bool> AddUserLikesPost(string userId, string postId);

        Task<bool> RemoveUserDislikesPost(string userId, string postId);

        Task<bool> DoesUserLikePost(string userId, string postId);

        List<UserWhoLikesAPostDTO> GetPeopleWhoLikePost(string postId); 
    }
}
