using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Services.LikesManagement
{
    public interface ILikesService
    {
        Task<bool> AddUserLikesPost(string userId, string postId);

        List<string> GetPeopleWhoLikePost(string postId); 
    }
}
