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
            this.UserPosts = new List<PostUsersProfileViewModel>();
        }

        public string UserId { get; set; }
        public string Name { get; set; }

        public List<PostUsersProfileViewModel> UserPosts { get; set; }

    }
}
