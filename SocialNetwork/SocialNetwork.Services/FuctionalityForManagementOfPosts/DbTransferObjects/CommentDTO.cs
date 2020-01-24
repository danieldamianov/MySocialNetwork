namespace SocialNetwork.Services.FuctionalityForManagementOfPosts.DbTransferObjects
{
    public class CommentDTO
    {
        public CommentDTO(string username, string content)
        {
            Username = username;
            Content = content;
        }

        public string Username { get; set; }

        public string Content { get; set; }
    }
}