using Microsoft.EntityFrameworkCore.Migrations;

namespace BKind.Migrations
{
    public partial class Mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonID",
                table: "Address",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
