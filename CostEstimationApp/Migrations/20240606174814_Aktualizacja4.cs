using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class Aktualizacja4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccuracyClasses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "IT1");

            migrationBuilder.UpdateData(
                table: "AccuracyClasses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "IT2");

            migrationBuilder.UpdateData(
                table: "AccuracyClasses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "IT3");

            migrationBuilder.InsertData(
                table: "AccuracyClasses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "IT4" },
                    { 5, "IT5" },
                    { 6, "IT6" },
                    { 7, "IT7" },
                    { 8, "IT8" },
                    { 9, "IT9" },
                    { 10, "IT10" },
                    { 11, "IT11" },
                    { 12, "IT12" },
                    { 13, "IT13" }
                });

            migrationBuilder.UpdateData(
                table: "SurfaceRoughnesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Ra4");

            migrationBuilder.UpdateData(
                table: "SurfaceRoughnesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Ra10");

            migrationBuilder.UpdateData(
                table: "SurfaceRoughnesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Ra20");

            migrationBuilder.InsertData(
                table: "SurfaceRoughnesses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Ra0.63" },
                    { 5, "Ra0.32" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccuracyClasses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AccuracyClasses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AccuracyClasses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AccuracyClasses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AccuracyClasses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AccuracyClasses",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AccuracyClasses",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AccuracyClasses",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AccuracyClasses",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AccuracyClasses",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "SurfaceRoughnesses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SurfaceRoughnesses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "AccuracyClasses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "IT12");

            migrationBuilder.UpdateData(
                table: "AccuracyClasses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "IT13");

            migrationBuilder.UpdateData(
                table: "AccuracyClasses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "IT14");

            migrationBuilder.UpdateData(
                table: "SurfaceRoughnesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "N10");

            migrationBuilder.UpdateData(
                table: "SurfaceRoughnesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "N11");

            migrationBuilder.UpdateData(
                table: "SurfaceRoughnesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "N12");
        }
    }
}
