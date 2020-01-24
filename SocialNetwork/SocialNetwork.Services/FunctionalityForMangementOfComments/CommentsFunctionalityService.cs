using SocialNetwork.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Services.FunctionalityForMangementOfComments
{
    public class CommentsFunctionalityService
    {
        public CommentsFunctionalityService(SocialNetworkDbContext socialNetworkDbContext)
        {
            this.socialNetworkDbContext = socialNetworkDbContext;
        }

        private readonly SocialNetworkDbContext socialNetworkDbContext;

        public void AddCommentToPost(string creatorId, string postId,string content)
        {
            this.socialNetworkDbContext.Comments.Add(new DatabaseModels.Comment(content, creatorId, postId));
            this.socialNetworkDbContext.SaveChanges();
        }
    }
}
