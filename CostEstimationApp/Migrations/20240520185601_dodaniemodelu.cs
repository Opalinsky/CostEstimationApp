using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class dodaniemodelu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Machines");

            migrationBuilder.AddColumn<int>(
                name: "MachineTypeId",
                table: "Machines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MachineTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Typeof = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalTime = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Machines_MachineTypeId",
                table: "Machines",
                column: "MachineTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_MachineTypes_MachineTypeId",
                table: "Machines",
                column: "MachineTypeId",
                principalTable: "MachineTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_MachineTypes_MachineTypeId",
                table: "Machines");

            migrationBuilder.DropTable(
                name: "MachineTypes");

            migrationBuilder.DropIndex(
                name: "IX_Machines_MachineTypeId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "MachineTypeId",
                table: "Machines");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Machines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
