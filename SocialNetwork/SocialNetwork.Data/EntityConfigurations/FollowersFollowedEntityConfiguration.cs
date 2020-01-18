using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Data.EntityConfigurations
{
    public class FollowersFollowedEntityConfiguration : IEntityTypeConfiguration<FollowerFollowed>
    {
        public void Configure(EntityTypeBuilder<FollowerFollowed> builder)
        {
            builder.HasKey(followerFollowed => new object[] { followerFollowed.FollowedId, followerFollowed.FollowerId });

            builder.HasOne(followerFollowed => followerFollowed.Follower)
                .WithMany(user => user.Followed)
                .HasForeignKey(followerFollowed => followerFollowed.FollowerId);

            builder.HasOne(followerFollowed => followerFollowed.Followed)
                .WithMany(user => user.Followers)
                .HasForeignKey(followerFollowed => followerFollowed.FollowedId);
        }
    }
}
