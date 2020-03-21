using SocialNetwork.Data;

namespace SocialNetwork.Services.CommentsManagement
{
    public class CommentsFunctionalityService : ICommentsFunctionalityService
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
