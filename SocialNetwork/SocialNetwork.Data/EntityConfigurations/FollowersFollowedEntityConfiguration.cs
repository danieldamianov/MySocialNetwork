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
            builder.HasKey(followerFollowed => new { followerFollowed.FollowerId, followerFollowed.FollowedId });

            builder.HasOne(followerFollowed => followerFollowed.Follower)
                .WithMany(user => user.Followed)
                .HasForeignKey(followerFollowed => followerFollowed.FollowerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(followerFollowed => followerFollowed.Followed)
                .WithMany(user => user.Followers)
                .HasForeignKey(followerFollowed => followerFollowed.FollowedId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
