using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class DodanieMRRFinish2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FinishSurfaceRoughness",
                table: "Przedmiots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoughSurfaceRoughness",
                table: "Przedmiots",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishSurfaceRoughness",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "RoughSurfaceRoughness",
                table: "Przedmiots");
        }
    }
}
