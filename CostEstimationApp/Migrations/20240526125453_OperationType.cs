using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class OperationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperationType",
                table: "Operations");

            migrationBuilder.AddColumn<int>(
                name: "OperationTypeId",
                table: "Operations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OperationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationTypes", x => x.Id);
                });

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

            migrationBuilder.InsertData(
                table: "OperationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Cutting" });

            migrationBuilder.InsertData(
                table: "OperationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Drilling" });

            migrationBuilder.CreateIndex(
                name: "IX_Operations_OperationTypeId",
                table: "Operations",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTypeMachine_OperationTypeId",
                table: "OperationTypeMachine",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTypeTool_ToolId",
                table: "OperationTypeTool",
                column: "ToolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_OperationTypes_OperationTypeId",
                table: "Operations",
                column: "OperationTypeId",
                principalTable: "OperationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_OperationTypes_OperationTypeId",
                table: "Operations");

            migrationBuilder.DropTable(
                name: "OperationTypeMachine");

            migrationBuilder.DropTable(
                name: "OperationTypeTool");

            migrationBuilder.DropTable(
                name: "OperationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Operations_OperationTypeId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "OperationTypeId",
                table: "Operations");

            migrationBuilder.AddColumn<string>(
                name: "OperationType",
                table: "Operations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
