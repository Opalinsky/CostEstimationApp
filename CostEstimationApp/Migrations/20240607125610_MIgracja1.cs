using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class MIgracja1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MachineTypes",
                columns: new[] { "Id", "AdditionalTime", "AuxiliaryTime", "Typeof" },
                values: new object[] { 1, 0.20000000000000001, 0.10000000000000001, "Automatyczna" });

            migrationBuilder.InsertData(
                table: "MachineTypes",
                columns: new[] { "Id", "AdditionalTime", "AuxiliaryTime", "Typeof" },
                values: new object[] { 2, 0.10000000000000001, 0.20000000000000001, "Konwencjonalna" });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "Density", "Name", "PricePerKg" },
                values: new object[] { 1, 7850m, "Stal", 8m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MachineTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MachineTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
