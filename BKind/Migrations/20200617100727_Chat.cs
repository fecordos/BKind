using Microsoft.EntityFrameworkCore.Migrations;

namespace BKind.Migrations
{
    public partial class Chat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndividualMessages_AspNetUsers_UserId",
                table: "IndividualMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndividualMessages",
                table: "IndividualMessages");

            migrationBuilder.DropIndex(
                name: "IX_IndividualMessages_UserId",
                table: "IndividualMessages");

            migrationBuilder.RenameTable(
                name: "IndividualMessages",
                newName: "IndividualMessage");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "IndividualMessage",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "IndividualMessage",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndividualMessage",
                table: "IndividualMessage",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualMessage_SenderId",
                table: "IndividualMessage",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualMessage_AspNetUsers_SenderId",
                table: "IndividualMessage",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndividualMessage_AspNetUsers_SenderId",
                table: "IndividualMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndividualMessage",
                table: "IndividualMessage");

            migrationBuilder.DropIndex(
                name: "IX_IndividualMessage_SenderId",
                table: "IndividualMessage");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "IndividualMessage");

            migrationBuilder.RenameTable(
                name: "IndividualMessage",
                newName: "IndividualMessages");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "IndividualMessages",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndividualMessages",
                table: "IndividualMessages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualMessages_UserId",
                table: "IndividualMessages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualMessages_AspNetUsers_UserId",
                table: "IndividualMessages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
