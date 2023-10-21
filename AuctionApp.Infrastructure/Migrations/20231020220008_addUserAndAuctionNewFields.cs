using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionApp.Infrastructure.Migrations
{
    public partial class addUserAndAuctionNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Budged",
                table: "User",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Auctions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_UserId",
                table: "Auctions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_User_UserId",
                table: "Auctions",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_User_UserId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_UserId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "Budged",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Auctions");
        }
    }
}
