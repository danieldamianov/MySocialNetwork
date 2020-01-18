using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data.EntityConfigurations;
using SocialNetwork.DatabaseModels;
using System;

namespace SocialNetwork.Data
{
    public class SocialNetworkDbContext : DbContext
    {
        public SocialNetworkDbContext()
        {

        }
        public DbSet<FollowerFollowed> FollowersFollowed { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<ImagePost> ImagePosts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CommentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FollowersFollowedEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ImagePostEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ReplyEntityConfiguration());
        }
    }
}
