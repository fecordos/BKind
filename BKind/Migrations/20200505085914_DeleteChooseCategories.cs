using Microsoft.EntityFrameworkCore.Migrations;

namespace BKind.Migrations
{
    public partial class DeleteChooseCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryAppUser");

            migrationBuilder.DropTable(
                name: "CheckBoxItem");

            migrationBuilder.DropTable(
                name: "CategoryAppUserViewModel");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Request",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Request",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "CategoryAppUser",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryAppUser", x => new { x.CategoryID, x.UserId });
                    table.ForeignKey(
                        name: "FK_CategoryAppUser_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryAppUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryAppUserViewModel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
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
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryAppUserViewModelID = table.Column<int>(type: "int", nullable: true),
                    IsChecked = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "IX_CategoryAppUser_UserId",
                table: "CategoryAppUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckBoxItem_CategoryAppUserViewModelID",
                table: "CheckBoxItem",
                column: "CategoryAppUserViewModelID");
        }
    }
}
