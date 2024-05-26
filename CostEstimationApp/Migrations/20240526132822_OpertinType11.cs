using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class OpertinType11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationTypeMachine");

            migrationBuilder.DropTable(
                name: "OperationTypeTool");

            migrationBuilder.CreateTable(
                name: "OperationTypeMachines",
                columns: table => new
                {
                    OperationTypeId = table.Column<int>(type: "int", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationTypeMachines", x => new { x.OperationTypeId, x.MachineId });
                    table.ForeignKey(
                        name: "FK_OperationTypeMachines_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationTypeMachines_OperationTypes_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalTable: "OperationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperationTypeTools",
                columns: table => new
                {
                    OperationTypeId = table.Column<int>(type: "int", nullable: false),
                    ToolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationTypeTools", x => new { x.OperationTypeId, x.ToolId });
                    table.ForeignKey(
                        name: "FK_OperationTypeTools_OperationTypes_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalTable: "OperationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationTypeTools_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationTypeMachines_MachineId",
                table: "OperationTypeMachines",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTypeTools_ToolId",
                table: "OperationTypeTools",
                column: "ToolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationTypeMachines");

            migrationBuilder.DropTable(
                name: "OperationTypeTools");

            migrationBuilder.CreateTable(
                name: "OperationTypeMachine",
                columns: table => new
                {
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    OperationTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationTypeMachine", x => new { x.MachineId, x.OperationTypeId });
                    table.ForeignKey(
                        name: "FK_OperationTypeMachine_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationTypeMachine_OperationTypes_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalTable: "OperationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperationTypeTool",
                columns: table => new
                {
                    OperationTypeId = table.Column<int>(type: "int", nullable: false),
                    ToolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationTypeTool", x => new { x.OperationTypeId, x.ToolId });
                    table.ForeignKey(
                        name: "FK_OperationTypeTool_OperationTypes_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalTable: "OperationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationTypeTool_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationTypeMachine_OperationTypeId",
                table: "OperationTypeMachine",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTypeTool_ToolId",
                table: "OperationTypeTool",
                column: "ToolId");
        }
    }
}
