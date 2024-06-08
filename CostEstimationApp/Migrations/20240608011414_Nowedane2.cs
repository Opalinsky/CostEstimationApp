using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class Nowedane2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_MRRs_MRRFinishId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Operations_MRRFinishId",
                table: "Operations");

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

            migrationBuilder.DropColumn(
                name: "FaceArea",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "FaceMillingDepth",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "FinishingMillingDepth",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "HeightAfterOperation",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "HeightBeforeOperation",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "LengthAfterOperation",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "LengthBeforeOperation",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "MRRFinishId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "PocketDepth",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "PocketLength",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "PocketWidth",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "WidthAfterOperation",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "WidthBeforeOperation",
                table: "Operations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<decimal>(
                name: "FaceArea",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FaceMillingDepth",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FinishingMillingDepth",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HeightAfterOperation",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HeightBeforeOperation",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LengthAfterOperation",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LengthBeforeOperation",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "MRRFinishId",
                table: "Operations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PocketDepth",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PocketLength",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PocketWidth",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WidthAfterOperation",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WidthBeforeOperation",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Operations_MRRFinishId",
                table: "Operations",
                column: "MRRFinishId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_MRRs_MRRFinishId",
                table: "Operations",
                column: "MRRFinishId",
                principalTable: "MRRs",
                principalColumn: "Id");
        }
    }
}
