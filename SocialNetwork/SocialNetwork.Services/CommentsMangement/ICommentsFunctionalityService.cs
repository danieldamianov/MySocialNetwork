using SocialNetwork.Services.CommentsMangement.DTOs;
using System.Collections.Generic;

namespace SocialNetwork.Services.CommentsManagement
{
    public interface ICommentsFunctionalityService
    {
        void AddCommentToPost(string creatorId, string postId, string content);

        IEnumerable<CommentedUserDTO> GetAllUsersWhoHaveCommentedPost(string postId);
    }
}
