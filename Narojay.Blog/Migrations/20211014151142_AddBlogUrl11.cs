using Microsoft.EntityFrameworkCore.Migrations;

namespace Narojay.Blog.Migrations
{
    public partial class AddBlogUrl11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LeaveMessage");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "LeaveMessage",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "LeaveMessage",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "LeaveMessage");

            migrationBuilder.DropColumn(
                name: "NickName",
                table: "LeaveMessage");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "LeaveMessage",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
