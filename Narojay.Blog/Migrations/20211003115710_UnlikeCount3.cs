using Microsoft.EntityFrameworkCore.Migrations;

namespace Narojay.Blog.Migrations
{
    public partial class UnlikeCount3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnLikeCount",
                table: "Post",
                newName: "UnlikeCount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnlikeCount",
                table: "Post",
                newName: "UnLikeCount");
        }
    }
}
