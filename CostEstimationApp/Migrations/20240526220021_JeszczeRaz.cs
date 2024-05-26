using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class JeszczeRaz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "OperationTypeId",
                table: "OperationTypeTools");

            migrationBuilder.DropColumn(
                name: "OperationTypeId",
                table: "OperationTypeMachines");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OperationTypeTools",
                newName: "ToolsId");

            migrationBuilder.RenameColumn(
                name: "ToolId",
                table: "OperationTypeTools",
                newName: "OperationTypesId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OperationTypeMachines",
                newName: "OperationTypesId");

            migrationBuilder.RenameColumn(
                name: "MachineId",
                table: "OperationTypeMachines",
                newName: "MachinesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperationTypeTools",
                table: "OperationTypeTools",
                columns: new[] { "OperationTypesId", "ToolsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperationTypeMachines",
                table: "OperationTypeMachines",
                columns: new[] { "MachinesId", "OperationTypesId" });

            migrationBuilder.CreateIndex(
                name: "IX_OperationTypeTools_ToolsId",
                table: "OperationTypeTools",
                column: "ToolsId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTypeMachines_OperationTypesId",
                table: "OperationTypeMachines",
                column: "OperationTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationTypeMachines_Machines_MachinesId",
                table: "OperationTypeMachines",
                column: "MachinesId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationTypeMachines_OperationTypes_OperationTypesId",
                table: "OperationTypeMachines",
                column: "OperationTypesId",
                principalTable: "OperationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationTypeTools_OperationTypes_OperationTypesId",
                table: "OperationTypeTools",
                column: "OperationTypesId",
                principalTable: "OperationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationTypeTools_Tools_ToolsId",
                table: "OperationTypeTools",
                column: "ToolsId",
                principalTable: "Tools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationTypeMachines_Machines_MachinesId",
                table: "OperationTypeMachines");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationTypeMachines_OperationTypes_OperationTypesId",
                table: "OperationTypeMachines");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationTypeTools_OperationTypes_OperationTypesId",
                table: "OperationTypeTools");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationTypeTools_Tools_ToolsId",
                table: "OperationTypeTools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OperationTypeTools",
                table: "OperationTypeTools");

            migrationBuilder.DropIndex(
                name: "IX_OperationTypeTools_ToolsId",
                table: "OperationTypeTools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OperationTypeMachines",
                table: "OperationTypeMachines");

            migrationBuilder.DropIndex(
                name: "IX_OperationTypeMachines_OperationTypesId",
                table: "OperationTypeMachines");

            migrationBuilder.RenameColumn(
                name: "ToolsId",
                table: "OperationTypeTools",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OperationTypesId",
                table: "OperationTypeTools",
                newName: "ToolId");

            migrationBuilder.RenameColumn(
                name: "OperationTypesId",
                table: "OperationTypeMachines",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MachinesId",
                table: "OperationTypeMachines",
                newName: "MachineId");

            migrationBuilder.AddColumn<int>(
                name: "OperationTypeId",
                table: "OperationTypeTools",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OperationTypeId",
                table: "OperationTypeMachines",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
