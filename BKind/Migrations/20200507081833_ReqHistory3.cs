using Microsoft.EntityFrameworkCore.Migrations;

namespace BKind.Migrations
{
    public partial class ReqHistory3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "ReqHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ReqHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "ReqHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonID",
                table: "ReqHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ReqHistory",
                type: "nvarchar(50)",
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
    }
}
