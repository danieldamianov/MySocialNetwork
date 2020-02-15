namespace SocialNetwork.Services.FuctionalityForManagementOfPosts.DbTransferObjects
{
    public class CommentDTO
    {
        public CommentDTO(string username, string content,string userId)
        {
            this.Username = username;
            this.Content = content;
            this.UserId = userId;
        }

        public string Username { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

    }
}