using SocialNetwork.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public string Photo { get; set; }

    }
}
