using Microsoft.EntityFrameworkCore.Migrations;

namespace BKind.Migrations
{
    public partial class OneToOneReqUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_AspNetUsers_AppUserId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_AppUserId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Request");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Request",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Request_UserId",
                table: "Request",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_AspNetUsers_UserId",
                table: "Request",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_AspNetUsers_UserId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_UserId",
                table: "Request");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Request",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Request_AppUserId",
                table: "Request",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_AspNetUsers_AppUserId",
                table: "Request",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
