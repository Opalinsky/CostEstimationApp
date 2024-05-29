using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class Pocket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PocketDepth",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PocketLength",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PocketWidth",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PocketDepth",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "PocketLength",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "PocketWidth",
                table: "Operations");
        }
    }
}
