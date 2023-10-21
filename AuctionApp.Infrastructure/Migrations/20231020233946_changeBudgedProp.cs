using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionApp.Infrastructure.Migrations
{
    public partial class changeBudgedProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Budged",
                table: "User",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Budged",
                table: "User",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
