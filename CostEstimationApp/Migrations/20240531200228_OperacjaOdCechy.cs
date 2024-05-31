using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class OperacjaOdCechy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FeatureId",
                table: "Operations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Operations_FeatureId",
                table: "Operations",
                column: "FeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Feature_FeatureId",
                table: "Operations",
                column: "FeatureId",
                principalTable: "Feature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Feature_FeatureId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Operations_FeatureId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "FeatureId",
                table: "Operations");
        }
    }
}
