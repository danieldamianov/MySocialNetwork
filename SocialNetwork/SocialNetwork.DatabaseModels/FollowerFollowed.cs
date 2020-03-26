namespace SocialNetwork.DatabaseModels
{
    public class FollowerFollowed
    {
        public FollowerFollowed(string followerId, string followedId)
        {
            this.FollowerId = followerId;
            this.FollowedId = followedId;
        }

        public string FollowerId { get; set; }

        public SocialNetworkUser Follower { get; set; }

        public string FollowedId { get; set; }

        public SocialNetworkUser Followed { get; set; }
    }
}
