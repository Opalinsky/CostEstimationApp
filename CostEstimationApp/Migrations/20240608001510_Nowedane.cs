using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class Nowedane : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Wiercenie Zgrubne");

            migrationBuilder.UpdateData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Wiercenie Wykańczające");

            migrationBuilder.UpdateData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Rozwiercanie");

            migrationBuilder.UpdateData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Frezowanie Zgrubne Kieszeni");

            migrationBuilder.UpdateData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Frezowanie Wykańczające Kieszeni");

            migrationBuilder.UpdateData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Frezowanie Zgrubnie Rowka");

            migrationBuilder.InsertData(
                table: "OperationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 9, "Frezowanie Wykańczająco Rowka" },
                    { 10, "Frezowanie Zgrubnie Uskoku" },
                    { 11, "Frezowanie Wykańczająco Uskoku" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Wiercenie");

            migrationBuilder.UpdateData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Rozwiercanie");

            migrationBuilder.UpdateData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Frezowanie Zgrubne Kieszeni");

            migrationBuilder.UpdateData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Frezowanie Wykańczające Kieszeni");

            migrationBuilder.UpdateData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Frezowanie Rowka");

            migrationBuilder.UpdateData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Frezowanie Uskoku");
        }
    }
}
