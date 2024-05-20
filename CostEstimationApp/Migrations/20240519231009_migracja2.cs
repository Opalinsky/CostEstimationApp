using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class migracja2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MRRs_MaterialId",
                table: "MRRs");

            migrationBuilder.DropIndex(
                name: "IX_MRRs_ToolId",
                table: "MRRs");

            migrationBuilder.DeleteData(
                table: "MRRs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MRRs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateIndex(
                name: "IX_MRRs_MaterialId",
                table: "MRRs",
                column: "MaterialId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MRRs_ToolId",
                table: "MRRs",
                column: "ToolId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MRRs_MaterialId",
                table: "MRRs");

            migrationBuilder.DropIndex(
                name: "IX_MRRs_ToolId",
                table: "MRRs");

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "Density", "Name", "PricePerKg" },
                values: new object[,]
                {
                    { 1, 7.85m, "Steel", 2.5m },
                    { 2, 2.70m, "Aluminum", 1.5m }
                });

            migrationBuilder.InsertData(
                table: "Tools",
                columns: new[] { "Id", "CostPerHour", "Name" },
                values: new object[,]
                {
                    { 1, 15.0m, "Drill" },
                    { 2, 20.0m, "Lathe" }
                });

            migrationBuilder.InsertData(
                table: "MRRs",
                columns: new[] { "Id", "MaterialId", "Rate", "ToolId" },
                values: new object[] { 1, 1, 0.5m, 1 });

            migrationBuilder.InsertData(
                table: "MRRs",
                columns: new[] { "Id", "MaterialId", "Rate", "ToolId" },
                values: new object[] { 2, 2, 0.7m, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_MRRs_MaterialId",
                table: "MRRs",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MRRs_ToolId",
                table: "MRRs",
                column: "ToolId");
        }
    }
}
