using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.Likes
{
    public class UserLikedOrUnlikedPostReturnModel
    {
        public UserLikedOrUnlikedPostReturnModel(
            int likesCount,
            List<UserLikedPostViewModel> usersLikedThePost,
            bool usersHasUnLikedThePost,
            bool usersHasLikedThePost)
        {
            this.LikesCount = likesCount;
            this.UsersLikedThePost = usersLikedThePost;
            this.UsersHasUnLikedThePost = usersHasUnLikedThePost;
            this.UsersHasLikedThePost = usersHasLikedThePost;
        }

        public int LikesCount { get; set; }

        public List<UserLikedPostViewModel> UsersLikedThePost { get; set; }

        public bool UsersHasUnLikedThePost { get; set; }

        public bool UsersHasLikedThePost { get; set; }
    }
}
