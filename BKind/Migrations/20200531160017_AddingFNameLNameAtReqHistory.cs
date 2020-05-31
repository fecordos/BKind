using Microsoft.EntityFrameworkCore.Migrations;

namespace BKind.Migrations
{
    public partial class AddingFNameLNameAtReqHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "ReqHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "ReqHistory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "ReqHistory");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "ReqHistory");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "ReqHistory",
                type: "nvarchar(450)",
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
    }
}
