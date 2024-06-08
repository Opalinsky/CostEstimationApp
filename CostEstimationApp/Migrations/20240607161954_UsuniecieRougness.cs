using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class UsuniecieRougness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Przedmiots_SurfaceRoughnesses_SurfaceRoughnessId",
                table: "Przedmiots");

            migrationBuilder.DropTable(
                name: "SurfaceRoughnesses");

            migrationBuilder.DropIndex(
                name: "IX_Przedmiots_SurfaceRoughnessId",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "FinishSurfaceRoughness",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "RoughSurfaceRoughness",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "SurfaceRoughnessId",
                table: "Przedmiots");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FinishSurfaceRoughness",
                table: "Przedmiots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoughSurfaceRoughness",
                table: "Przedmiots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SurfaceRoughnessId",
                table: "Przedmiots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SurfaceRoughnesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurfaceRoughnesses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SurfaceRoughnesses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ra4" },
                    { 2, "Ra10" },
                    { 3, "Ra20" },
                    { 4, "Ra0.63" },
                    { 5, "Ra0.32" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Przedmiots_SurfaceRoughnessId",
                table: "Przedmiots",
                column: "SurfaceRoughnessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Przedmiots_SurfaceRoughnesses_SurfaceRoughnessId",
                table: "Przedmiots",
                column: "SurfaceRoughnessId",
                principalTable: "SurfaceRoughnesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
