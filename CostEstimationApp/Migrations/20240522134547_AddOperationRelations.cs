using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class AddOperationRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Orders_OrderId",
                table: "Operations");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Operations",
                newName: "SemiFinishedProductId");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Operations",
                newName: "WidthBeforeOperation");

            migrationBuilder.RenameColumn(
                name: "DimensionZ",
                table: "Operations",
                newName: "WidthAfterOperation");

            migrationBuilder.RenameColumn(
                name: "DimensionY",
                table: "Operations",
                newName: "LengthBeforeOperation");

            migrationBuilder.RenameColumn(
                name: "DimensionX",
                table: "Operations",
                newName: "LengthAfterOperation");

            migrationBuilder.RenameIndex(
                name: "IX_Operations_OrderId",
                table: "Operations",
                newName: "IX_Operations_SemiFinishedProductId");

            migrationBuilder.AddColumn<decimal>(
                name: "HeightAfterOperation",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HeightBeforeOperation",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "OperationTypeId",
                table: "Operations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OperationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Typeof = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operations_OperationTypeId",
                table: "Operations",
                column: "OperationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_OperationType_OperationTypeId",
                table: "Operations",
                column: "OperationTypeId",
                principalTable: "OperationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_SemiFinishedProducts_SemiFinishedProductId",
                table: "Operations",
                column: "SemiFinishedProductId",
                principalTable: "SemiFinishedProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_OperationType_OperationTypeId",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_SemiFinishedProducts_SemiFinishedProductId",
                table: "Operations");

            migrationBuilder.DropTable(
                name: "OperationType");

            migrationBuilder.DropIndex(
                name: "IX_Operations_OperationTypeId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "HeightAfterOperation",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "HeightBeforeOperation",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "OperationTypeId",
                table: "Operations");

            migrationBuilder.RenameColumn(
                name: "WidthBeforeOperation",
                table: "Operations",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "WidthAfterOperation",
                table: "Operations",
                newName: "DimensionZ");

            migrationBuilder.RenameColumn(
                name: "SemiFinishedProductId",
                table: "Operations",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "LengthBeforeOperation",
                table: "Operations",
                newName: "DimensionY");

            migrationBuilder.RenameColumn(
                name: "LengthAfterOperation",
                table: "Operations",
                newName: "DimensionX");

            migrationBuilder.RenameIndex(
                name: "IX_Operations_SemiFinishedProductId",
                table: "Operations",
                newName: "IX_Operations_OrderId");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemiFinishedProductId = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PostHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PostLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PostWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_SemiFinishedProducts_SemiFinishedProductId",
                        column: x => x.SemiFinishedProductId,
                        principalTable: "SemiFinishedProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    MaterialCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperationCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SemiFinishedProductId",
                table: "Orders",
                column: "SemiFinishedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_OrderId",
                table: "Results",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Orders_OrderId",
                table: "Operations",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
