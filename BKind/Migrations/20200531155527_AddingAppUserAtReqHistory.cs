using Microsoft.EntityFrameworkCore.Migrations;

namespace BKind.Migrations
{
    public partial class AddingAppUserAtReqHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "ReqHistory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReqHistory_AppUserId",
                table: "ReqHistory",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReqHistory_AspNetUsers_AppUserId",
                table: "ReqHistory",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReqHistory_AspNetUsers_AppUserId",
                table: "ReqHistory");

            migrationBuilder.DropIndex(
                name: "IX_ReqHistory_AppUserId",
                table: "ReqHistory");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ReqHistory");
        }
    }
}
