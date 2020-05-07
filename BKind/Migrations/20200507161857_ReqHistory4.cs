using Microsoft.EntityFrameworkCore.Migrations;

namespace BKind.Migrations
{
    public partial class ReqHistory4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "ReqHistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ReqHistory",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "ReqHistory",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonID",
                table: "ReqHistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ReqHistory",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ReqHistory_CategoryID",
                table: "ReqHistory",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ReqHistory_PersonID",
                table: "ReqHistory",
                column: "PersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReqHistory_Category_CategoryID",
                table: "ReqHistory",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReqHistory_Person_PersonID",
                table: "ReqHistory",
                column: "PersonID",
                principalTable: "Person",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReqHistory_Category_CategoryID",
                table: "ReqHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReqHistory_Person_PersonID",
                table: "ReqHistory");

            migrationBuilder.DropIndex(
                name: "IX_ReqHistory_CategoryID",
                table: "ReqHistory");

            migrationBuilder.DropIndex(
                name: "IX_ReqHistory_PersonID",
                table: "ReqHistory");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "ReqHistory");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ReqHistory");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "ReqHistory");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "ReqHistory");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ReqHistory");
        }
    }
}
