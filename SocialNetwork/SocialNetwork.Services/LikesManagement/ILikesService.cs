using System.Collections.Generic;
using System.Threading.Tasks;

using SocialNetwork.Services.LikesManagement.DTOs;

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
