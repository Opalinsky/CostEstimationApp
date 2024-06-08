using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class DodanieMRRFinish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MRRFinishId",
                table: "Operations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VolumeToRemoveFinish",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RateFinish",
                table: "MRRs",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_MRRs_MRRFinishId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Operations_MRRFinishId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "MRRFinishId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "VolumeToRemoveFinish",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "RateFinish",
                table: "MRRs");
        }
    }
}
