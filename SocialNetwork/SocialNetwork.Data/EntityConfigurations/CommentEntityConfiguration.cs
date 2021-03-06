﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Data.EntityConfigurations
{
    public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {// AddedImageAndVideoEntitiesAndDecoupledPostsFromThemAndChangedCascadeBehaviourForCommentsForeignKeys
            builder
                .HasOne(comment => comment.Creator)
                .WithMany(user => user.Comments)
                .HasForeignKey(comment => comment.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
               .HasOne(comment => comment.Post)
               .WithMany(post => post.Comments)
               .HasForeignKey(comment => comment.PostId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
