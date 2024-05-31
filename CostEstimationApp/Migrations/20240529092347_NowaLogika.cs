using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class NowaLogika : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcesId",
                table: "OperationSets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Projekts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SemiFinishedProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projekts_SemiFinishedProducts_SemiFinishedProductId",
                        column: x => x.SemiFinishedProductId,
                        principalTable: "SemiFinishedProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Przedmiots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjektId = table.Column<int>(type: "int", nullable: false),
                    Cechy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przedmiots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Przedmiots_Projekts_ProjektId",
                        column: x => x.ProjektId,
                        principalTable: "Projekts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Process",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreparationTime = table.Column<decimal>(type: "decimal(18,12)", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    MachineCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WorkerCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,12)", nullable: false),
                    PrzedmiotId = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Projekts_SemiFinishedProductId",
                table: "Projekts",
                column: "SemiFinishedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Przedmiots_ProjektId",
                table: "Przedmiots",
                column: "ProjektId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationSets_Process_ProcesId",
                table: "OperationSets",
                column: "ProcesId",
                principalTable: "Process",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationSets_Process_ProcesId",
                table: "OperationSets");

            migrationBuilder.DropTable(
                name: "Process");

            migrationBuilder.DropTable(
                name: "Przedmiots");

            migrationBuilder.DropTable(
                name: "Projekts");

            migrationBuilder.DropIndex(
                name: "IX_OperationSets_ProcesId",
                table: "OperationSets");

            migrationBuilder.DropColumn(
                name: "ProcesId",
                table: "OperationSets");
        }
    }
}
