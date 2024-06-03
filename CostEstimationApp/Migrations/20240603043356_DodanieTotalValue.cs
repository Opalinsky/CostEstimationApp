using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class DodanieTotalValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalValue",
                table: "Projekts",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalValue",
                table: "Projekts");
        }
    }
}
