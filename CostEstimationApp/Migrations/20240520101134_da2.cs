using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class da2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MRRs_Materials_MaterialId",
                table: "MRRs");

            migrationBuilder.DropForeignKey(
                name: "FK_MRRs_Tools_ToolId",
                table: "MRRs");

            migrationBuilder.AddForeignKey(
                name: "FK_MRRs_Materials_MaterialId",
                table: "MRRs",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MRRs_Tools_ToolId",
                table: "MRRs",
                column: "ToolId",
                principalTable: "Tools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MRRs_Materials_MaterialId",
                table: "MRRs");

            migrationBuilder.DropForeignKey(
                name: "FK_MRRs_Tools_ToolId",
                table: "MRRs");

            migrationBuilder.AddForeignKey(
                name: "FK_MRRs_Materials_MaterialId",
                table: "MRRs",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MRRs_Tools_ToolId",
                table: "MRRs",
                column: "ToolId",
                principalTable: "Tools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
