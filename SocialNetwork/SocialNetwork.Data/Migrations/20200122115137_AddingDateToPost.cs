using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Data.Migrations
{
    public partial class AddingDateToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FollowersFollowed",
                table: "FollowersFollowed");

            migrationBuilder.DropIndex(
                name: "IX_FollowersFollowed_FollowerId",
                table: "FollowersFollowed");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeCreated",
                table: "ImagePosts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FollowersFollowed",
                table: "FollowersFollowed",
                columns: new[] { "FollowerId", "FollowedId" });

            migrationBuilder.CreateIndex(
                name: "IX_FollowersFollowed_FollowedId",
                table: "FollowersFollowed",
                column: "FollowedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FollowersFollowed",
                table: "FollowersFollowed");

            migrationBuilder.DropIndex(
                name: "IX_FollowersFollowed_FollowedId",
                table: "FollowersFollowed");

            migrationBuilder.DropColumn(
                name: "DateTimeCreated",
                table: "ImagePosts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FollowersFollowed",
                table: "FollowersFollowed",
                columns: new[] { "FollowedId", "FollowerId" });

            migrationBuilder.CreateIndex(
                name: "IX_FollowersFollowed_FollowerId",
                table: "FollowersFollowed",
                column: "FollowerId");
        }
    }
}
