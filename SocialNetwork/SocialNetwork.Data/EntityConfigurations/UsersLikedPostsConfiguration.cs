using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Data.EntityConfigurations
{
    public class UsersLikedPostsConfiguration : IEntityTypeConfiguration<UsersLikedPosts>
    {
        public void Configure(EntityTypeBuilder<UsersLikedPosts> builder)
        {
            builder.HasKey(userLikedPost => new { userLikedPost.UserId, userLikedPost.PostId });

            builder.HasOne(userLikedPost => userLikedPost.User)
                .WithMany(user => user.LikedPosts)
                .HasForeignKey(userLikedPost => userLikedPost.UserId);

            builder.HasOne(userLikedPost => userLikedPost.Post)
                .WithMany(post => post.UsersLikedThePost)
                .HasForeignKey(userLikedPost => userLikedPost.PostId);
        }
    }
}
