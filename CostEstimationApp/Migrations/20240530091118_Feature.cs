using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class Feature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cechy",
                table: "Przedmiots");

            migrationBuilder.AddColumn<decimal>(
                name: "AddFinishingMilling",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AddFinishingOperation",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DrillApplicationCount",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DrillDepth",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DrillDiameter",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FaceMillingDepth",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FeatureId",
                table: "Przedmiots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "FinishingMillingDepth",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PocketDepth",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PocketLength",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PocketWidth",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SlotApplicationCount",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SlotHeight",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WhichSurface",
                table: "Przedmiots",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Przedmiots_FeatureId",
                table: "Przedmiots",
                column: "FeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Przedmiots_Feature_FeatureId",
                table: "Przedmiots",
                column: "FeatureId",
                principalTable: "Feature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Przedmiots_Feature_FeatureId",
                table: "Przedmiots");

            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropIndex(
                name: "IX_Przedmiots_FeatureId",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "AddFinishingMilling",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "AddFinishingOperation",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "DrillApplicationCount",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "DrillDepth",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "DrillDiameter",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "FaceMillingDepth",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "FeatureId",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "FinishingMillingDepth",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "PocketDepth",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "PocketLength",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "PocketWidth",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "SlotApplicationCount",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "SlotHeight",
                table: "Przedmiots");

            migrationBuilder.DropColumn(
                name: "WhichSurface",
                table: "Przedmiots");

            migrationBuilder.AddColumn<string>(
                name: "Cechy",
                table: "Przedmiots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
