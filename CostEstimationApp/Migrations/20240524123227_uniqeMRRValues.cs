using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class uniqeMRRValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MRRs_MaterialId",
                table: "MRRs");

            migrationBuilder.CreateIndex(
                name: "IX_MRRs_MaterialId_ToolMaterialId",
                table: "MRRs",
                columns: new[] { "MaterialId", "ToolMaterialId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MRRs_MaterialId_ToolMaterialId",
                table: "MRRs");

            migrationBuilder.CreateIndex(
                name: "IX_MRRs_MaterialId",
                table: "MRRs",
                column: "MaterialId");
        }
    }
}
