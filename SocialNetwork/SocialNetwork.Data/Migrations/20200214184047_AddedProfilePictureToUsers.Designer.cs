﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialNetwork.Data;

namespace SocialNetwork.Data.Migrations
{
    [DbContext(typeof(SocialNetworkDbContext))]
    [Migration("20200214184047_AddedProfilePictureToUsers")]
    partial class AddedProfilePictureToUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SocialNetwork.DatabaseModels.Comment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PostId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SocialNetwork.DatabaseModels.FollowerFollowed", b =>
                {
                    b.Property<string>("FollowerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FollowedId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FollowerId", "FollowedId");

                    b.HasIndex("FollowedId");

                    b.ToTable("FollowersFollowed");
                });

            modelBuilder.Entity("SocialNetwork.DatabaseModels.ImagePost", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateTimeCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("ImagePosts");
                });

            modelBuilder.Entity("SocialNetwork.DatabaseModels.Reply", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CommentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("SocialNetwork.DatabaseModels.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SocialNetwork.DatabaseModels.Comment", b =>
                {
                    b.HasOne("SocialNetwork.DatabaseModels.User", "Creator")
                        .WithMany("Comments")
                        .HasForeignKey("CreatorId");

                    b.HasOne("SocialNetwork.DatabaseModels.ImagePost", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("SocialNetwork.DatabaseModels.FollowerFollowed", b =>
                {
                    b.HasOne("SocialNetwork.DatabaseModels.User", "Followed")
                        .WithMany("Followers")
                        .HasForeignKey("FollowedId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SocialNetwork.DatabaseModels.User", "Follower")
                        .WithMany("Followed")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("SocialNetwork.DatabaseModels.ImagePost", b =>
                {
                    b.HasOne("SocialNetwork.DatabaseModels.User", "Creator")
                        .WithMany("Posts")
                        .HasForeignKey("CreatorId");
                });

            modelBuilder.Entity("SocialNetwork.DatabaseModels.Reply", b =>
                {
                    b.HasOne("SocialNetwork.DatabaseModels.Comment", "Comment")
                        .WithMany("Replies")
                        .HasForeignKey("CommentId");

                    b.HasOne("SocialNetwork.DatabaseModels.User", "Creator")
                        .WithMany("Replies")
                        .HasForeignKey("CreatorId");
                });
#pragma warning restore 612, 618
        }
    }
}