using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class DodanieWymiarówDoCechy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasPreviousFeature",
                table: "Przedmiots",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "HeightAfterOperation",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HeightBeforeOperation",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LengthAfterOperation",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LengthBeforeOperation",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WidthAfterOperation",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WidthBeforeOperation",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPreviousFeature",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "HeightAfterOperation",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "HeightBeforeOperation",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "LengthAfterOperation",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "LengthBeforeOperation",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "WidthAfterOperation",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "WidthBeforeOperation",
                table: "Przedmiots");
        }
    }
}
