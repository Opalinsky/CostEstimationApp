using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CuttingDepth",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CuttingLength",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CuttingWidth",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DrillDepth",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DrillDiameter",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CuttingDepth",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "CuttingLength",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "CuttingWidth",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "DrillDepth",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "DrillDiameter",
                table: "Operations");
        }
    }
}
