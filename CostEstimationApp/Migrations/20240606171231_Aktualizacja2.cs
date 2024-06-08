using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class Aktualizacja2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationSets_Process_ProcesId",
                table: "OperationSets");

            migrationBuilder.DropTable(
                name: "Process");

            migrationBuilder.DropIndex(
                name: "IX_OperationSets_ProcesId",
                table: "OperationSets");

            migrationBuilder.DropColumn(
                name: "ProcesId",
                table: "OperationSets");

            migrationBuilder.AddColumn<string>(
                name: "FinishAccuracyClass",
                table: "Przedmiots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinishSurfaceRoughness",
                table: "Przedmiots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoughAccuracyClass",
                table: "Przedmiots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoughSurfaceRoughness",
                table: "Przedmiots",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishAccuracyClass",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "FinishSurfaceRoughness",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "RoughAccuracyClass",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "RoughSurfaceRoughness",
                table: "Przedmiots");

            migrationBuilder.AddColumn<int>(
                name: "ProcesId",
                table: "OperationSets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Process",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    MachineCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreparationTime = table.Column<decimal>(type: "decimal(18,12)", nullable: false),
                    PrzedmiotId = table.Column<int>(type: "int", nullable: true),
                    ToolCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,12)", nullable: false),
                    WorkerCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Process", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Process_Przedmiots_PrzedmiotId",
                        column: x => x.PrzedmiotId,
                        principalTable: "Przedmiots",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Process_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationSets_ProcesId",
                table: "OperationSets",
                column: "ProcesId");

            migrationBuilder.CreateIndex(
                name: "IX_Process_PrzedmiotId",
                table: "Process",
                column: "PrzedmiotId");

            migrationBuilder.CreateIndex(
                name: "IX_Process_WorkerId",
                table: "Process",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationSets_Process_ProcesId",
                table: "OperationSets",
                column: "ProcesId",
                principalTable: "Process",
                principalColumn: "Id");
        }
    }
}
