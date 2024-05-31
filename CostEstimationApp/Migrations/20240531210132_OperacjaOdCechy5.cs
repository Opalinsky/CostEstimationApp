using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class OperacjaOdCechy5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureOperationType");

            migrationBuilder.CreateTable(
                name: "FeatureOperationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    OperationTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureOperationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureOperationTypes_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureOperationTypes_OperationTypes_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalTable: "OperationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureOperationTypes_FeatureId",
                table: "FeatureOperationTypes",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureOperationTypes_OperationTypeId",
                table: "FeatureOperationTypes",
                column: "OperationTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureOperationTypes");

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
                        name: "FK_FeatureOperationType_Features_FeaturesId",
                        column: x => x.FeaturesId,
                        principalTable: "Features",
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
    }
}
