using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class RzeczyWTabeli : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Frezowanie Czołowe" },
                    { 2, "Wiercenie" }
                });

            migrationBuilder.InsertData(
                table: "MachineTypes",
                columns: new[] { "Id", "AdditionalTime", "AuxiliaryTime", "Typeof" },
                values: new object[,]
                {
                    { 1, 0.20000000000000001, 0.10000000000000001, "Automatyczna" },
                    { 2, 0.10000000000000001, 0.20000000000000001, "Manualna" }
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "Density", "Name", "PricePerKg" },
                values: new object[] { 1, 0.1m, "Material1", 8m });

            migrationBuilder.InsertData(
                table: "OperationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Face Milling" },
                    { 2, "Finishing Milling" }
                });

            migrationBuilder.InsertData(
                table: "ToolMaterials",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Stal Szybkotnąca" });

            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "Id", "CostPerHour", "Name" },
                values: new object[] { 1, 50m, "Pracownika1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 2);

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

            migrationBuilder.DeleteData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ToolMaterials",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
