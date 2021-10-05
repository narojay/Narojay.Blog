using Microsoft.EntityFrameworkCore.Migrations;

namespace Narojay.Blog.Migrations
{
    public partial class likecount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTop",
                table: "Post",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Post",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnLikeCount",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTop",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "UnLikeCount",
                table: "Post");
        }
    }
}
