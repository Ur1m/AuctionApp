using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionApp.Infrastructure.Migrations
{
    public partial class addBidderUserIdForAuction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BidderUserId",
                table: "Auctions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BidderUserId",
                table: "Auctions");
        }
    }
}
