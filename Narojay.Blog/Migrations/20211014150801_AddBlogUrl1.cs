using Microsoft.EntityFrameworkCore.Migrations;

namespace Narojay.Blog.Migrations
{
    public partial class AddBlogUrl1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveMessage_User_UserId",
                table: "LeaveMessage");

            migrationBuilder.DropIndex(
                name: "IX_LeaveMessage_UserId",
                table: "LeaveMessage");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "LeaveMessage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveMessage_ParentId",
                table: "LeaveMessage",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveMessage_LeaveMessage_ParentId",
                table: "LeaveMessage",
                column: "ParentId",
                principalTable: "LeaveMessage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveMessage_LeaveMessage_ParentId",
                table: "LeaveMessage");

            migrationBuilder.DropIndex(
                name: "IX_LeaveMessage_ParentId",
                table: "LeaveMessage");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "LeaveMessage");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveMessage_UserId",
                table: "LeaveMessage",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveMessage_User_UserId",
                table: "LeaveMessage",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
