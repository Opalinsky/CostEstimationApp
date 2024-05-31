using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class SessionToOperationSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjektId",
                table: "OperationSets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OperationSets_ProjektId",
                table: "OperationSets",
                column: "ProjektId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationSets_Projekts_ProjektId",
                table: "OperationSets",
                column: "ProjektId",
                principalTable: "Projekts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationSets_Projekts_ProjektId",
                table: "OperationSets");

            migrationBuilder.DropIndex(
                name: "IX_OperationSets_ProjektId",
                table: "OperationSets");

            migrationBuilder.DropColumn(
                name: "ProjektId",
                table: "OperationSets");
        }
    }
}
