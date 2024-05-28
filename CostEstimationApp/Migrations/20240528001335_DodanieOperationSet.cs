using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class DodanieOperationSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OperationSetId",
                table: "Operations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OperationSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PreparationTime = table.Column<decimal>(type: "decimal(18,12)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,12)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationSets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operations_OperationSetId",
                table: "Operations",
                column: "OperationSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_OperationSets_OperationSetId",
                table: "Operations",
                column: "OperationSetId",
                principalTable: "OperationSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_OperationSets_OperationSetId",
                table: "Operations");

            migrationBuilder.DropTable(
                name: "OperationSets");

            migrationBuilder.DropIndex(
                name: "IX_Operations_OperationSetId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "OperationSetId",
                table: "Operations");
        }
    }
}
