using Microsoft.EntityFrameworkCore.Migrations;

namespace BKind.Migrations
{
    public partial class ChooseCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryAppUserViewModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryAppUserViewModel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CheckBoxItem",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    IsChecked = table.Column<bool>(nullable: false),
                    CategoryAppUserViewModelID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckBoxItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CheckBoxItem_CategoryAppUserViewModel_CategoryAppUserViewModelID",
                        column: x => x.CategoryAppUserViewModelID,
                        principalTable: "CategoryAppUserViewModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckBoxItem_CategoryAppUserViewModelID",
                table: "CheckBoxItem",
                column: "CategoryAppUserViewModelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckBoxItem");

            migrationBuilder.DropTable(
                name: "CategoryAppUserViewModel");
        }
    }
}
