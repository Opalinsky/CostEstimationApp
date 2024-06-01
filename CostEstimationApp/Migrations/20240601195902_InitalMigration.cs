using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostEstimationApp.Migrations
{
    public partial class InitalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MachineTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Typeof = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalTime = table.Column<double>(type: "float", nullable: false),
                    AuxiliaryTime = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Density = table.Column<decimal>(type: "decimal(18,12)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToolMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolMaterials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostPerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostPerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MachineTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Machines_MachineTypes_MachineTypeId",
                        column: x => x.MachineTypeId,
                        principalTable: "MachineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SemiFinishedProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    DimensionX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionZ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemiFinishedProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SemiFinishedProducts_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeatureOperationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureId = table.Column<int>(type: "int", nullable: true),
                    OperationTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureOperationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureOperationTypes_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeatureOperationTypes_OperationTypes_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalTable: "OperationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MRRs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    ToolMaterialId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MRRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MRRs_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MRRs_ToolMaterials_ToolMaterialId",
                        column: x => x.ToolMaterialId,
                        principalTable: "ToolMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountOfEdges = table.Column<int>(type: "int", nullable: false),
                    VitalityPerEdge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostPerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolMaterialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tools_ToolMaterials_ToolMaterialId",
                        column: x => x.ToolMaterialId,
                        principalTable: "ToolMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OperationTypeMachines",
                columns: table => new
                {
                    MachinesId = table.Column<int>(type: "int", nullable: false),
                    OperationTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationTypeMachines", x => new { x.MachinesId, x.OperationTypesId });
                    table.ForeignKey(
                        name: "FK_OperationTypeMachines_Machines_MachinesId",
                        column: x => x.MachinesId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationTypeMachines_OperationTypes_OperationTypesId",
                        column: x => x.OperationTypesId,
                        principalTable: "OperationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projekts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SemiFinishedProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projekts_SemiFinishedProducts_SemiFinishedProductId",
                        column: x => x.SemiFinishedProductId,
                        principalTable: "SemiFinishedProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OperationTypeTools",
                columns: table => new
                {
                    OperationTypesId = table.Column<int>(type: "int", nullable: false),
                    ToolsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationTypeTools", x => new { x.OperationTypesId, x.ToolsId });
                    table.ForeignKey(
                        name: "FK_OperationTypeTools_OperationTypes_OperationTypesId",
                        column: x => x.OperationTypesId,
                        principalTable: "OperationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationTypeTools_Tools_ToolsId",
                        column: x => x.ToolsId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Przedmiots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjektId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    DrillDiameter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DrillDepth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DrillApplicationCount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FaceMillingDepth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinishingMillingDepth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AddFinishingMilling = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PocketLength = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PocketWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PocketDepth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AddFinishingOperation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SlotHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WhichSurface = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SlotApplicationCount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przedmiots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Przedmiots_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Przedmiots_Projekts_ProjektId",
                        column: x => x.ProjektId,
                        principalTable: "Projekts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Process",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreparationTime = table.Column<decimal>(type: "decimal(18,12)", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    MachineCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WorkerCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,12)", nullable: false),
                    PrzedmiotId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Process", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Process_Przedmiots_PrzedmiotId",
                        column: x => x.PrzedmiotId,
                        principalTable: "Przedmiots",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Process_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperationSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WorkerCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,12)", nullable: false),
                    ProjektId = table.Column<int>(type: "int", nullable: false),
                    ProcesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationSets_Process_ProcesId",
                        column: x => x.ProcesId,
                        principalTable: "Process",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OperationSets_Projekts_ProjektId",
                        column: x => x.ProjektId,
                        principalTable: "Projekts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SemiFinishedProductId = table.Column<int>(type: "int", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    ProjektId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    ToolId = table.Column<int>(type: "int", nullable: false),
                    OperationTypeId = table.Column<int>(type: "int", nullable: false),
                    MRRId = table.Column<int>(type: "int", nullable: false),
                    OperationSetId = table.Column<int>(type: "int", nullable: true),
                    CuttingLength = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CuttingWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CuttingDepth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DrillDiameter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DrillDepth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinishingMillingDepth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FaceArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FaceMillingDepth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PocketLength = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PocketWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PocketDepth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LengthBeforeOperation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WidthBeforeOperation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HeightBeforeOperation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LengthAfterOperation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WidthAfterOperation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HeightAfterOperation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VolumeToRemove = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MachiningTime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MachineCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WorkerCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operations_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operations_MRRs_MRRId",
                        column: x => x.MRRId,
                        principalTable: "MRRs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operations_OperationSets_OperationSetId",
                        column: x => x.OperationSetId,
                        principalTable: "OperationSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operations_OperationTypes_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalTable: "OperationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operations_Projekts_ProjektId",
                        column: x => x.ProjektId,
                        principalTable: "Projekts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operations_SemiFinishedProducts_SemiFinishedProductId",
                        column: x => x.SemiFinishedProductId,
                        principalTable: "SemiFinishedProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operations_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operations_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureOperationTypes_FeatureId",
                table: "FeatureOperationTypes",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureOperationTypes_OperationTypeId",
                table: "FeatureOperationTypes",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_MachineTypeId",
                table: "Machines",
                column: "MachineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MRRs_MaterialId_ToolMaterialId",
                table: "MRRs",
                columns: new[] { "MaterialId", "ToolMaterialId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MRRs_ToolMaterialId",
                table: "MRRs",
                column: "ToolMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_FeatureId",
                table: "Operations",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_MachineId",
                table: "Operations",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_MRRId",
                table: "Operations",
                column: "MRRId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_OperationSetId",
                table: "Operations",
                column: "OperationSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_OperationTypeId",
                table: "Operations",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_ProjektId",
                table: "Operations",
                column: "ProjektId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_SemiFinishedProductId",
                table: "Operations",
                column: "SemiFinishedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_ToolId",
                table: "Operations",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_WorkerId",
                table: "Operations",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationSets_ProcesId",
                table: "OperationSets",
                column: "ProcesId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationSets_ProjektId",
                table: "OperationSets",
                column: "ProjektId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTypeMachines_OperationTypesId",
                table: "OperationTypeMachines",
                column: "OperationTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTypeTools_ToolsId",
                table: "OperationTypeTools",
                column: "ToolsId");

            migrationBuilder.CreateIndex(
                name: "IX_Process_PrzedmiotId",
                table: "Process",
                column: "PrzedmiotId");

            migrationBuilder.CreateIndex(
                name: "IX_Process_WorkerId",
                table: "Process",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projekts_SemiFinishedProductId",
                table: "Projekts",
                column: "SemiFinishedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Przedmiots_FeatureId",
                table: "Przedmiots",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Przedmiots_ProjektId",
                table: "Przedmiots",
                column: "ProjektId");

            migrationBuilder.CreateIndex(
                name: "IX_SemiFinishedProducts_MaterialId",
                table: "SemiFinishedProducts",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_ToolMaterialId",
                table: "Tools",
                column: "ToolMaterialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureOperationTypes");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "OperationTypeMachines");

            migrationBuilder.DropTable(
                name: "OperationTypeTools");

            migrationBuilder.DropTable(
                name: "MRRs");

            migrationBuilder.DropTable(
                name: "OperationSets");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "OperationTypes");

            migrationBuilder.DropTable(
                name: "Tools");

            migrationBuilder.DropTable(
                name: "Process");

            migrationBuilder.DropTable(
                name: "MachineTypes");

            migrationBuilder.DropTable(
                name: "ToolMaterials");

            migrationBuilder.DropTable(
                name: "Przedmiots");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Projekts");

            migrationBuilder.DropTable(
                name: "SemiFinishedProducts");

            migrationBuilder.DropTable(
                name: "Materials");
        }
    }
}
