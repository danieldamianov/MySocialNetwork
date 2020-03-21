namespace SocialNetwork.Models.Home
{
    public class UserLikedPostHomeIndexViewModel
    {
        public UserLikedPostHomeIndexViewModel(string userName, string id, string avatar)
        {
            this.UserName = userName;
            this.Id = id;
            this.Avatar = avatar;
        }

        public string UserName { get; set; }

        public string Id { get; set; }

        public string Avatar { get; set; }
    }
}
