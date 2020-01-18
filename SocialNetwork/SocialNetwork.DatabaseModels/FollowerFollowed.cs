namespace SocialNetwork.DatabaseModels
{
    public class FollowerFollowed
    {
        public FollowerFollowed(string followerId, string followedId)
        {
            FollowerId = followerId;
            FollowedId = followedId;
        }

        public string FollowerId { get; set; }
        public User Follower { get; set; }

        public string FollowedId { get; set; }
        public User Followed { get; set; }
    }
}
