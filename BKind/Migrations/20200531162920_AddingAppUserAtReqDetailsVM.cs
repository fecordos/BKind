using Microsoft.EntityFrameworkCore.Migrations;

namespace BKind.Migrations
{
    public partial class AddingAppUserAtReqDetailsVM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "ReqHistory");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "ReqHistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "ReqHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "ReqHistory",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
