using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class Aktualizacja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Przedmiots_AccuracyClass_AccuracyClassId",
                table: "Przedmiots");

            migrationBuilder.DropForeignKey(
                name: "FK_Przedmiots_FinishingAccuracyClass_FinishingAccuracyClassId",
                table: "Przedmiots");

            migrationBuilder.DropForeignKey(
                name: "FK_Przedmiots_FinishingSurfaceRoughness_FinishingSurfaceRoughnessId",
                table: "Przedmiots");

            migrationBuilder.DropForeignKey(
                name: "FK_Przedmiots_SurfaceRoughness_SurfaceRoughnessId",
                table: "Przedmiots");

            migrationBuilder.DropTable(
                name: "FinishingAccuracyClass");

            migrationBuilder.DropTable(
                name: "FinishingSurfaceRoughness");

            migrationBuilder.DropIndex(
                name: "IX_Przedmiots_FinishingAccuracyClassId",
                table: "Przedmiots");

            migrationBuilder.DropIndex(
                name: "IX_Przedmiots_FinishingSurfaceRoughnessId",
                table: "Przedmiots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurfaceRoughness",
                table: "SurfaceRoughness");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccuracyClass",
                table: "AccuracyClass");

            migrationBuilder.DropColumn(
                name: "FinishingAccuracyClassId",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "FinishingSurfaceRoughnessId",
                table: "Przedmiots");

            migrationBuilder.RenameTable(
                name: "SurfaceRoughness",
                newName: "SurfaceRoughnesses");

            migrationBuilder.RenameTable(
                name: "AccuracyClass",
                newName: "AccuracyClasses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurfaceRoughnesses",
                table: "SurfaceRoughnesses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccuracyClasses",
                table: "AccuracyClasses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Przedmiots_AccuracyClasses_AccuracyClassId",
                table: "Przedmiots",
                column: "AccuracyClassId",
                principalTable: "AccuracyClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Przedmiots_SurfaceRoughnesses_SurfaceRoughnessId",
                table: "Przedmiots",
                column: "SurfaceRoughnessId",
                principalTable: "SurfaceRoughnesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Przedmiots_AccuracyClasses_AccuracyClassId",
                table: "Przedmiots");

            migrationBuilder.DropForeignKey(
                name: "FK_Przedmiots_SurfaceRoughnesses_SurfaceRoughnessId",
                table: "Przedmiots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurfaceRoughnesses",
                table: "SurfaceRoughnesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccuracyClasses",
                table: "AccuracyClasses");

            migrationBuilder.RenameTable(
                name: "SurfaceRoughnesses",
                newName: "SurfaceRoughness");

            migrationBuilder.RenameTable(
                name: "AccuracyClasses",
                newName: "AccuracyClass");

            migrationBuilder.AddColumn<int>(
                name: "FinishingAccuracyClassId",
                table: "Przedmiots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FinishingSurfaceRoughnessId",
                table: "Przedmiots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurfaceRoughness",
                table: "SurfaceRoughness",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccuracyClass",
                table: "AccuracyClass",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FinishingAccuracyClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishingAccuracyClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinishingSurfaceRoughness",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishingSurfaceRoughness", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FinishingAccuracyClass",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "IT5" },
                    { 2, "IT6" },
                    { 3, "IT7" }
                });

            migrationBuilder.InsertData(
                table: "FinishingSurfaceRoughness",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "N4" },
                    { 2, "N5" },
                    { 3, "N6" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Przedmiots_FinishingAccuracyClassId",
                table: "Przedmiots",
                column: "FinishingAccuracyClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Przedmiots_FinishingSurfaceRoughnessId",
                table: "Przedmiots",
                column: "FinishingSurfaceRoughnessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Przedmiots_AccuracyClass_AccuracyClassId",
                table: "Przedmiots",
                column: "AccuracyClassId",
                principalTable: "AccuracyClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Przedmiots_FinishingAccuracyClass_FinishingAccuracyClassId",
                table: "Przedmiots",
                column: "FinishingAccuracyClassId",
                principalTable: "FinishingAccuracyClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Przedmiots_FinishingSurfaceRoughness_FinishingSurfaceRoughnessId",
                table: "Przedmiots",
                column: "FinishingSurfaceRoughnessId",
                principalTable: "FinishingSurfaceRoughness",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Przedmiots_SurfaceRoughness_SurfaceRoughnessId",
                table: "Przedmiots",
                column: "SurfaceRoughnessId",
                principalTable: "SurfaceRoughness",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
