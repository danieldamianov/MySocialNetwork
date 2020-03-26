using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DatabaseModels;

namespace SocialNetwork.Data.EntityConfigurations
{
    internal class PostEntityConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasOne(post => post.Creator)
                .WithMany(user => user.Posts)
                .HasForeignKey(post => post.CreatorId);

            builder.HasMany(post => post.Images)
                .WithOne(image => image.Post)
                .HasForeignKey(image => image.PostId);

            builder.HasMany(post => post.Videos)
                .WithOne(video => video.Post)
                .HasForeignKey(video => video.PostId);
        }
    }
}
