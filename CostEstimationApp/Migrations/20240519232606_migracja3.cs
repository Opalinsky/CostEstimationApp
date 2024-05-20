using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class migracja3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MRRs_MaterialId",
                table: "MRRs");

            migrationBuilder.DropIndex(
                name: "IX_MRRs_ToolId",
                table: "MRRs");

            migrationBuilder.CreateIndex(
                name: "IX_MRRs_MaterialId",
                table: "MRRs",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MRRs_ToolId",
                table: "MRRs",
                column: "ToolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MRRs_MaterialId",
                table: "MRRs");

            migrationBuilder.DropIndex(
                name: "IX_MRRs_ToolId",
                table: "MRRs");

            migrationBuilder.CreateIndex(
                name: "IX_MRRs_MaterialId",
                table: "MRRs",
                column: "MaterialId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MRRs_ToolId",
                table: "MRRs",
                column: "ToolId",
                unique: true);
        }
    }
}
