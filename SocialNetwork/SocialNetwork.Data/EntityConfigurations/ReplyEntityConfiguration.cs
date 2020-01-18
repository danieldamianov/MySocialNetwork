using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Data.EntityConfigurations
{
    public class ReplyEntityConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            builder.HasOne(reply => reply.Comment)
                .WithMany(comment => comment.Replies)
                .HasForeignKey(reply => reply.CommentId);

            builder.HasOne(reply => reply.Creator)
                .WithMany(comment => comment.Replies)
                .HasForeignKey(reply => reply.CreatorId);
        }
    }
}
