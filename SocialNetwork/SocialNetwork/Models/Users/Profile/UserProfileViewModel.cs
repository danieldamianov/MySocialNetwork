using System.Collections.Generic;

using SocialNetwork.Models.Home;

namespace SocialNetwork.Models.Users.Profile
{
    public class UserProfileViewModel
    {
        public UserProfileViewModel()
        {
            this.UserPosts = new List<PostHomeIndexViewModel>();
        }

        public string UserId { get; set; }

        public string Name { get; set; }

        public List<PostHomeIndexViewModel> UserPosts { get; set; }

        public string ProfilePicturePath { get; set; }

    }
}
