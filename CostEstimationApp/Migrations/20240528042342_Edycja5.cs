using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class Edycja5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationSets_Workers_WorkerId",
                table: "OperationSets");

            migrationBuilder.DropIndex(
                name: "IX_OperationSets_WorkerId",
                table: "OperationSets");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "OperationSets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "OperationSets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OperationSets_WorkerId",
                table: "OperationSets",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationSets_Workers_WorkerId",
                table: "OperationSets",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
