using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class EdycjaModeli : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_OperationTypes_OperationTypeId",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationTypeMachines_Machines_MachineId",
                table: "OperationTypeMachines");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationTypeMachines_OperationTypes_OperationTypeId",
                table: "OperationTypeMachines");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationTypeTools_OperationTypes_OperationTypeId",
                table: "OperationTypeTools");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationTypeTools_Tools_ToolId",
                table: "OperationTypeTools");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_ToolMaterials_ToolMaterialId",
                table: "Tools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OperationTypeTools",
                table: "OperationTypeTools");

            migrationBuilder.DropIndex(
                name: "IX_OperationTypeTools_ToolId",
                table: "OperationTypeTools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OperationTypeMachines",
                table: "OperationTypeMachines");

            migrationBuilder.DropIndex(
                name: "IX_OperationTypeMachines_MachineId",
                table: "OperationTypeMachines");

            migrationBuilder.RenameColumn(
                name: "ToolId",
                table: "OperationTypeTools",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MachineId",
                table: "OperationTypeMachines",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "ToolMaterialId",
                table: "Tools",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "OperationTypeTools",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "OperationTypeMachines",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperationTypeTools",
                table: "OperationTypeTools",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperationTypeMachines",
                table: "OperationTypeMachines",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MachineOperationTypeMachine",
                columns: table => new
                {
                    MachinesId = table.Column<int>(type: "int", nullable: false),
                    OperationTypeMachinesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineOperationTypeMachine", x => new { x.MachinesId, x.OperationTypeMachinesId });
                    table.ForeignKey(
                        name: "FK_MachineOperationTypeMachine_Machines_MachinesId",
                        column: x => x.MachinesId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineOperationTypeMachine_OperationTypeMachines_OperationTypeMachinesId",
                        column: x => x.OperationTypeMachinesId,
                        principalTable: "OperationTypeMachines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperationTypeToolTool",
                columns: table => new
                {
                    OperationTypeToolsId = table.Column<int>(type: "int", nullable: false),
                    ToolsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationTypeToolTool", x => new { x.OperationTypeToolsId, x.ToolsId });
                    table.ForeignKey(
                        name: "FK_OperationTypeToolTool_OperationTypeTools_OperationTypeToolsId",
                        column: x => x.OperationTypeToolsId,
                        principalTable: "OperationTypeTools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationTypeToolTool_Tools_ToolsId",
                        column: x => x.ToolsId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationTypeTools_OperationTypeId",
                table: "OperationTypeTools",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTypeMachines_OperationTypeId",
                table: "OperationTypeMachines",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineOperationTypeMachine_OperationTypeMachinesId",
                table: "MachineOperationTypeMachine",
                column: "OperationTypeMachinesId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTypeToolTool_ToolsId",
                table: "OperationTypeToolTool",
                column: "ToolsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_OperationTypes_OperationTypeId",
                table: "Operations",
                column: "OperationTypeId",
                principalTable: "OperationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationTypeMachines_OperationTypes_OperationTypeId",
                table: "OperationTypeMachines",
                column: "OperationTypeId",
                principalTable: "OperationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationTypeTools_OperationTypes_OperationTypeId",
                table: "OperationTypeTools",
                column: "OperationTypeId",
                principalTable: "OperationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_ToolMaterials_ToolMaterialId",
                table: "Tools",
                column: "ToolMaterialId",
                principalTable: "ToolMaterials",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_OperationTypes_OperationTypeId",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationTypeMachines_OperationTypes_OperationTypeId",
                table: "OperationTypeMachines");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationTypeTools_OperationTypes_OperationTypeId",
                table: "OperationTypeTools");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_ToolMaterials_ToolMaterialId",
                table: "Tools");

            migrationBuilder.DropTable(
                name: "MachineOperationTypeMachine");

            migrationBuilder.DropTable(
                name: "OperationTypeToolTool");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OperationTypeTools",
                table: "OperationTypeTools");

            migrationBuilder.DropIndex(
                name: "IX_OperationTypeTools_OperationTypeId",
                table: "OperationTypeTools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OperationTypeMachines",
                table: "OperationTypeMachines");

            migrationBuilder.DropIndex(
                name: "IX_OperationTypeMachines_OperationTypeId",
                table: "OperationTypeMachines");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OperationTypeTools",
                newName: "ToolId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OperationTypeMachines",
                newName: "MachineId");

            migrationBuilder.AlterColumn<int>(
                name: "ToolMaterialId",
                table: "Tools",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ToolId",
                table: "OperationTypeTools",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "OperationTypeMachines",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperationTypeTools",
                table: "OperationTypeTools",
                columns: new[] { "OperationTypeId", "ToolId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperationTypeMachines",
                table: "OperationTypeMachines",
                columns: new[] { "OperationTypeId", "MachineId" });

            migrationBuilder.CreateIndex(
                name: "IX_OperationTypeTools_ToolId",
                table: "OperationTypeTools",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTypeMachines_MachineId",
                table: "OperationTypeMachines",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_OperationTypes_OperationTypeId",
                table: "Operations",
                column: "OperationTypeId",
                principalTable: "OperationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationTypeMachines_Machines_MachineId",
                table: "OperationTypeMachines",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationTypeMachines_OperationTypes_OperationTypeId",
                table: "OperationTypeMachines",
                column: "OperationTypeId",
                principalTable: "OperationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationTypeTools_OperationTypes_OperationTypeId",
                table: "OperationTypeTools",
                column: "OperationTypeId",
                principalTable: "OperationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationTypeTools_Tools_ToolId",
                table: "OperationTypeTools",
                column: "ToolId",
                principalTable: "Tools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_ToolMaterials_ToolMaterialId",
                table: "Tools",
                column: "ToolMaterialId",
                principalTable: "ToolMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
