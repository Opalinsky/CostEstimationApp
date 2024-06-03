using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class WorkerToMachine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Workers_WorkerId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Operations_WorkerId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Operations");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Workers",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "Machines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Machines_WorkerId",
                table: "Machines",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Workers_WorkerId",
                table: "Machines",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Workers_WorkerId",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Machines_WorkerId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Machines");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Workers",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "Operations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Operations_WorkerId",
                table: "Operations",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Workers_WorkerId",
                table: "Operations",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
