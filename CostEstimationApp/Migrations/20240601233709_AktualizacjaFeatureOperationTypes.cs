using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class AktualizacjaFeatureOperationTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeatureOperationTypes_Features_FeatureId",
                table: "FeatureOperationTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureOperationTypes_OperationTypes_OperationTypeId",
                table: "FeatureOperationTypes");

            migrationBuilder.AlterColumn<int>(
                name: "OperationTypeId",
                table: "FeatureOperationTypes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FeatureId",
                table: "FeatureOperationTypes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureOperationTypes_Features_FeatureId",
                table: "FeatureOperationTypes",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureOperationTypes_OperationTypes_OperationTypeId",
                table: "FeatureOperationTypes",
                column: "OperationTypeId",
                principalTable: "OperationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeatureOperationTypes_Features_FeatureId",
                table: "FeatureOperationTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureOperationTypes_OperationTypes_OperationTypeId",
                table: "FeatureOperationTypes");

            migrationBuilder.AlterColumn<int>(
                name: "OperationTypeId",
                table: "FeatureOperationTypes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FeatureId",
                table: "FeatureOperationTypes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureOperationTypes_Features_FeatureId",
                table: "FeatureOperationTypes",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureOperationTypes_OperationTypes_OperationTypeId",
                table: "FeatureOperationTypes",
                column: "OperationTypeId",
                principalTable: "OperationTypes",
                principalColumn: "Id");
        }
    }
}
