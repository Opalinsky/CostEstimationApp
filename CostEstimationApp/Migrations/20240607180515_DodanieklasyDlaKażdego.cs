using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class DodanieklasyDlaKażdego : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishAccuracyClass",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "FinishSurfaceRoughness",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "RoughAccuracyClass",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "RoughSurfaceRoughness",
                table: "Przedmiots");

            migrationBuilder.AddColumn<decimal>(
                name: "DrillingDepthFinish",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SlotHeightFinish",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StepHeightFinish",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrillingDepthFinish",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "SlotHeightFinish",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "StepHeightFinish",
                table: "Przedmiots");

            migrationBuilder.AddColumn<string>(
                name: "FinishAccuracyClass",
                table: "Przedmiots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinishSurfaceRoughness",
                table: "Przedmiots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoughAccuracyClass",
                table: "Przedmiots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoughSurfaceRoughness",
                table: "Przedmiots",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
