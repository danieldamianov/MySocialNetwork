using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.Users.Profile
{
    public class PostUsersProfileViewModel
    {
        public string PostId { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string Code { get; set; }
    }
}
