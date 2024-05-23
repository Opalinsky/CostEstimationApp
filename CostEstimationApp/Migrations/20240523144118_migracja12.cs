using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class migracja12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MRRs_Tools_ToolId",
                table: "MRRs");

            migrationBuilder.DropForeignKey(
                name: "FK_SemiFinishedProducts_Materials_MaterialId",
                table: "SemiFinishedProducts");

            migrationBuilder.RenameColumn(
                name: "ToolId",
                table: "MRRs",
                newName: "ToolMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_MRRs_ToolId",
                table: "MRRs",
                newName: "IX_MRRs_ToolMaterialId");

            migrationBuilder.AddColumn<int>(
                name: "ToolMaterialId",
                table: "Tools",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ToolMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolMaterials", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tools_ToolMaterialId",
                table: "Tools",
                column: "ToolMaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_MRRs_ToolMaterials_ToolMaterialId",
                table: "MRRs",
                column: "ToolMaterialId",
                principalTable: "ToolMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SemiFinishedProducts_Materials_MaterialId",
                table: "SemiFinishedProducts",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_ToolMaterials_ToolMaterialId",
                table: "Tools",
                column: "ToolMaterialId",
                principalTable: "ToolMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MRRs_ToolMaterials_ToolMaterialId",
                table: "MRRs");

            migrationBuilder.DropForeignKey(
                name: "FK_SemiFinishedProducts_Materials_MaterialId",
                table: "SemiFinishedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_ToolMaterials_ToolMaterialId",
                table: "Tools");

            migrationBuilder.DropTable(
                name: "ToolMaterials");

            migrationBuilder.DropIndex(
                name: "IX_Tools_ToolMaterialId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "ToolMaterialId",
                table: "Tools");

            migrationBuilder.RenameColumn(
                name: "ToolMaterialId",
                table: "MRRs",
                newName: "ToolId");

            migrationBuilder.RenameIndex(
                name: "IX_MRRs_ToolMaterialId",
                table: "MRRs",
                newName: "IX_MRRs_ToolId");

            migrationBuilder.AddForeignKey(
                name: "FK_MRRs_Tools_ToolId",
                table: "MRRs",
                column: "ToolId",
                principalTable: "Tools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SemiFinishedProducts_Materials_MaterialId",
                table: "SemiFinishedProducts",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
