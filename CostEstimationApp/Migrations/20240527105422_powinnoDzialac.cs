using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class powinnoDzialac : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_ToolMaterials_ToolMaterialId",
                table: "Tools");

            migrationBuilder.AlterColumn<int>(
                name: "ToolMaterialId",
                table: "Tools",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                name: "FK_Tools_ToolMaterials_ToolMaterialId",
                table: "Tools");

            migrationBuilder.AlterColumn<int>(
                name: "ToolMaterialId",
                table: "Tools",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_ToolMaterials_ToolMaterialId",
                table: "Tools",
                column: "ToolMaterialId",
                principalTable: "ToolMaterials",
                principalColumn: "Id");
        }
    }
}
