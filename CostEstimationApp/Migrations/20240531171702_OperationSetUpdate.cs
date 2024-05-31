using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class OperationSetUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreparationTime",
                table: "OperationSets");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OperationSets");

            migrationBuilder.CreateTable(
                name: "FeatureOperationType",
                columns: table => new
                {
                    FeaturesId = table.Column<int>(type: "int", nullable: false),
                    OperationTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureOperationType", x => new { x.FeaturesId, x.OperationTypesId });
                    table.ForeignKey(
                        name: "FK_FeatureOperationType_Feature_FeaturesId",
                        column: x => x.FeaturesId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureOperationType_OperationTypes_OperationTypesId",
                        column: x => x.OperationTypesId,
                        principalTable: "OperationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureOperationType_OperationTypesId",
                table: "FeatureOperationType",
                column: "OperationTypesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureOperationType");

            migrationBuilder.AddColumn<decimal>(
                name: "PreparationTime",
                table: "OperationSets",
                type: "decimal(18,12)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OperationSets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
