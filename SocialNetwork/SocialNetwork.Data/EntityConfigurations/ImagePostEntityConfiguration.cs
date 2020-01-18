using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DatabaseModels;

namespace SocialNetwork.Data.EntityConfigurations
{
    internal class ImagePostEntityConfiguration : IEntityTypeConfiguration<ImagePost>
    {
        public void Configure(EntityTypeBuilder<ImagePost> builder)
        {
            builder.HasOne(post => post.Creator)
                .WithMany(user => user.Posts)
                .HasForeignKey(post => post.CreatorId);
        }
    }
}
