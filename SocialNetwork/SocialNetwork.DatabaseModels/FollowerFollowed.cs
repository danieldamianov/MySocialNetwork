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
        public SocialNetworkUser Follower { get; set; }

        public string FollowedId { get; set; }
        public SocialNetworkUser Followed { get; set; }
    }
}
