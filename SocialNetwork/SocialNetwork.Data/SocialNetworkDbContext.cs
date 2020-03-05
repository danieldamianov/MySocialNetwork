using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data.EntityConfigurations;
using SocialNetwork.DatabaseModels;
using System;

namespace SocialNetwork.Data
{
    public class SocialNetworkDbContext : IdentityDbContext<SocialNetworkUser>
    {
        public SocialNetworkDbContext(DbContextOptions<SocialNetworkDbContext> options)
        : base(options)
        { }

        public DbSet<FollowerFollowed> FollowersFollowed { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Reply> Replies { get; set; }

        public DbSet<ImagePost> ImagePosts { get; set; }

        public DbSet<UsersLikedPosts> UsersLikedPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CommentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FollowersFollowedEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ImagePostEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ReplyEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UsersLikedPostsConfiguration());
        }
    }
}
