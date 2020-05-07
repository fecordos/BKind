using Microsoft.EntityFrameworkCore.Migrations;

namespace BKind.Migrations
{
    public partial class ReqHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReqHistoryRequestId",
                table: "Request",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReqHistoryUserId",
                table: "Request",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReqHistory",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RequestId = table.Column<int>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReqHistory", x => new { x.UserId, x.RequestId });
                    table.ForeignKey(
                        name: "FK_ReqHistory_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Request_ReqHistoryUserId_ReqHistoryRequestId",
                table: "Request",
                columns: new[] { "ReqHistoryUserId", "ReqHistoryRequestId" });

            migrationBuilder.CreateIndex(
                name: "IX_ReqHistory_AppUserId",
                table: "ReqHistory",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_ReqHistory_ReqHistoryUserId_ReqHistoryRequestId",
                table: "Request",
                columns: new[] { "ReqHistoryUserId", "ReqHistoryRequestId" },
                principalTable: "ReqHistory",
                principalColumns: new[] { "UserId", "RequestId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_ReqHistory_ReqHistoryUserId_ReqHistoryRequestId",
                table: "Request");

            migrationBuilder.DropTable(
                name: "ReqHistory");

            migrationBuilder.DropIndex(
                name: "IX_Request_ReqHistoryUserId_ReqHistoryRequestId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "ReqHistoryRequestId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "ReqHistoryUserId",
                table: "Request");
        }
    }
}
