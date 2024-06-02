using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class niewiwempoco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Projekts_ProjektId",
                table: "Operations");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Projekts_ProjektId",
                table: "Operations",
                column: "ProjektId",
                principalTable: "Projekts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Projekts_ProjektId",
                table: "Operations");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Projekts_ProjektId",
                table: "Operations",
                column: "ProjektId",
                principalTable: "Projekts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
