using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class OperacjaOdCechy4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeatureOperationType_Feature_FeaturesId",
                table: "FeatureOperationType");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Feature_FeatureId",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "FK_Przedmiots_Feature_FeatureId",
                table: "Przedmiots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feature",
                table: "Feature");

            migrationBuilder.RenameTable(
                name: "Feature",
                newName: "Features");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Features",
                table: "Features",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureOperationType_Features_FeaturesId",
                table: "FeatureOperationType",
                column: "FeaturesId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Features_FeatureId",
                table: "Operations",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Przedmiots_Features_FeatureId",
                table: "Przedmiots",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeatureOperationType_Features_FeaturesId",
                table: "FeatureOperationType");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Features_FeatureId",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "FK_Przedmiots_Features_FeatureId",
                table: "Przedmiots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Features",
                table: "Features");

            migrationBuilder.RenameTable(
                name: "Features",
                newName: "Feature");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feature",
                table: "Feature",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureOperationType_Feature_FeaturesId",
                table: "FeatureOperationType",
                column: "FeaturesId",
                principalTable: "Feature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Feature_FeatureId",
                table: "Operations",
                column: "FeatureId",
                principalTable: "Feature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Przedmiots_Feature_FeatureId",
                table: "Przedmiots",
                column: "FeatureId",
                principalTable: "Feature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
