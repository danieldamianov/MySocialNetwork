using SocialNetwork.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Services.CommentsManagement
{
    public interface ICommentsFunctionalityService
    {
        void AddCommentToPost(string creatorId, string postId, string content);
    }
}
