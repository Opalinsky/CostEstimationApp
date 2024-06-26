﻿// <auto-generated />
using System;
using CostEstimationApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CostEstimationApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240607161954_UsuniecieRougness")]
    partial class UsuniecieRougness
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CostEstimationApp.Models.AccuracyClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AccuracyClasses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "IT1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "IT2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "IT3"
                        },
                        new
                        {
                            Id = 4,
                            Name = "IT4"
                        },
                        new
                        {
                            Id = 5,
                            Name = "IT5"
                        },
                        new
                        {
                            Id = 6,
                            Name = "IT6"
                        },
                        new
                        {
                            Id = 7,
                            Name = "IT7"
                        },
                        new
                        {
                            Id = 8,
                            Name = "IT8"
                        },
                        new
                        {
                            Id = 9,
                            Name = "IT9"
                        },
                        new
                        {
                            Id = 10,
                            Name = "IT10"
                        },
                        new
                        {
                            Id = 11,
                            Name = "IT11"
                        },
                        new
                        {
                            Id = 12,
                            Name = "IT12"
                        },
                        new
                        {
                            Id = 13,
                            Name = "IT13"
                        });
                });

            modelBuilder.Entity("CostEstimationApp.Models.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Features");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Płaszczyzna Górna"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Otwór"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Kieszeń Zamknięta"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Rowek Przelotowy"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Uskok"
                        });
                });

            modelBuilder.Entity("CostEstimationApp.Models.FeatureOperationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("FeatureId")
                        .HasColumnType("int");

                    b.Property<int>("OperationTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId");

                    b.HasIndex("OperationTypeId");

                    b.ToTable("FeatureOperationTypes");
                });

            modelBuilder.Entity("CostEstimationApp.Models.Machine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("CostPerHour")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MachineTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MachineTypeId");

                    b.HasIndex("WorkerId");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("CostEstimationApp.Models.MachineType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("AdditionalTime")
                        .HasColumnType("float");

                    b.Property<double>("AuxiliaryTime")
                        .HasColumnType("float");

                    b.Property<string>("Typeof")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MachineTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AdditionalTime = 0.20000000000000001,
                            AuxiliaryTime = 0.10000000000000001,
                            Typeof = "Automatyczna"
                        },
                        new
                        {
                            Id = 2,
                            AdditionalTime = 0.10000000000000001,
                            AuxiliaryTime = 0.20000000000000001,
                            Typeof = "Konwencjonalna"
                        });
                });

            modelBuilder.Entity("CostEstimationApp.Models.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Density")
                        .HasColumnType("decimal(18,12)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PricePerKg")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Materials");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Density = 7850m,
                            Name = "Stal",
                            PricePerKg = 8m
                        });
                });

            modelBuilder.Entity("CostEstimationApp.Models.MRR", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ToolMaterialId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ToolMaterialId");

                    b.HasIndex("MaterialId", "ToolMaterialId")
                        .IsUnique();

                    b.ToTable("MRRs");
                });

            modelBuilder.Entity("CostEstimationApp.Models.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal?>("CuttingDepth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("CuttingLength")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("CuttingWidth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DrillDepth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DrillDiameter")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("FaceArea")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("FaceMillingDepth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("FeatureId")
                        .HasColumnType("int");

                    b.Property<decimal?>("FinishingMillingDepth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("HeightAfterOperation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("HeightBeforeOperation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LengthAfterOperation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LengthBeforeOperation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MRRId")
                        .HasColumnType("int");

                    b.Property<decimal>("MachineCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<decimal>("MachiningTime")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OperationSetId")
                        .HasColumnType("int");

                    b.Property<int>("OperationTypeId")
                        .HasColumnType("int");

                    b.Property<decimal?>("PocketDepth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("PocketLength")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("PocketWidth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProjektId")
                        .HasColumnType("int");

                    b.Property<int>("SemiFinishedProductId")
                        .HasColumnType("int");

                    b.Property<decimal?>("SetUpTime")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ToolCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ToolId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("VolumeToRemove")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("WidthAfterOperation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("WidthBeforeOperation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("WorkerCost")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId");

                    b.HasIndex("MRRId");

                    b.HasIndex("MachineId");

                    b.HasIndex("OperationSetId");

                    b.HasIndex("OperationTypeId");

                    b.HasIndex("ProjektId");

                    b.HasIndex("SemiFinishedProductId");

                    b.HasIndex("ToolId");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("CostEstimationApp.Models.OperationSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("MachineCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjektId")
                        .HasColumnType("int");

                    b.Property<decimal>("ToolCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(18,12)");

                    b.Property<decimal>("WorkerCost")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ProjektId");

                    b.ToTable("OperationSets");
                });

            modelBuilder.Entity("CostEstimationApp.Models.OperationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OperationTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Frezowanie Zgrubne Płaszczyzny Górnej"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Frezowanie Wykańczające Płaszczyzny"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Wiercenie"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Rozwiercanie"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Frezowanie Zgrubne Kieszeni"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Frezowanie Wykańczające Kieszeni"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Frezowanie Rowka"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Frezowanie Uskoku"
                        });
                });

            modelBuilder.Entity("CostEstimationApp.Models.SemiFinishedProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("DimensionX")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DimensionY")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DimensionZ")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Volume")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.ToTable("SemiFinishedProducts");
                });

            modelBuilder.Entity("CostEstimationApp.Models.Tool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AmountOfEdges")
                        .HasColumnType("int");

                    b.Property<decimal>("CostPerHour")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ToolMaterialId")
                        .HasColumnType("int");

                    b.Property<decimal>("VitalityPerEdge")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ToolMaterialId");

                    b.ToTable("Tools");
                });

            modelBuilder.Entity("CostEstimationApp.Models.ToolMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ToolMaterials");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Stal Szybkotnąca"
                        });
                });

            modelBuilder.Entity("CostEstimationApp.Models.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("CostPerHour")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Workers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CostPerHour = 50m,
                            Name = "Pracownika1"
                        });
                });

            modelBuilder.Entity("MachineOperationType", b =>
                {
                    b.Property<int>("MachinesId")
                        .HasColumnType("int");

                    b.Property<int>("OperationTypesId")
                        .HasColumnType("int");

                    b.HasKey("MachinesId", "OperationTypesId");

                    b.HasIndex("OperationTypesId");

                    b.ToTable("OperationTypeMachines", (string)null);
                });

            modelBuilder.Entity("OperationTypeTool", b =>
                {
                    b.Property<int>("OperationTypesId")
                        .HasColumnType("int");

                    b.Property<int>("ToolsId")
                        .HasColumnType("int");

                    b.HasKey("OperationTypesId", "ToolsId");

                    b.HasIndex("ToolsId");

                    b.ToTable("OperationTypeTools", (string)null);
                });

            modelBuilder.Entity("Projekt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OperationCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("SemiFinishedProductCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SemiFinishedProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("SemiFinishedProductId");

                    b.ToTable("Projekts");
                });

            modelBuilder.Entity("Przedmiot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccuracyClassId")
                        .HasColumnType("int");

                    b.Property<decimal?>("AddFinishingOperation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DrillApplicationCount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DrillDepth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DrillDiameter")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("FaceMillingDepth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("FeatureId")
                        .HasColumnType("int");

                    b.Property<string>("FinishAccuracyClass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("FinishingMillingDepth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("HasPreviousFeature")
                        .HasColumnType("bit");

                    b.Property<decimal>("HeightAfterOperation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("HeightBeforeOperation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LengthAfterOperation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LengthBeforeOperation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PocketDepth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("PocketLength")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("PocketWidth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProjektId")
                        .HasColumnType("int");

                    b.Property<decimal?>("ReamingDepth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("ReamingDiameter")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RoughAccuracyClass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("SlotApplicationCount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("SlotHeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool?>("SlotPlane")
                        .HasColumnType("bit");

                    b.Property<decimal?>("StepHeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool?>("StepPlane")
                        .HasColumnType("bit");

                    b.Property<decimal?>("StepWidth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("VolumeToRemove")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("VolumeToRemoveFinish")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("WidthAfterOperation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("WidthBeforeOperation")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AccuracyClassId");

                    b.HasIndex("FeatureId");

                    b.HasIndex("ProjektId");

                    b.ToTable("Przedmiots");
                });

            modelBuilder.Entity("CostEstimationApp.Models.FeatureOperationType", b =>
                {
                    b.HasOne("CostEstimationApp.Models.Feature", "Feature")
                        .WithMany("FeatureOperationTypes")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CostEstimationApp.Models.OperationType", "OperationType")
                        .WithMany("FeatureOperationTypes")
                        .HasForeignKey("OperationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feature");

                    b.Navigation("OperationType");
                });

            modelBuilder.Entity("CostEstimationApp.Models.Machine", b =>
                {
                    b.HasOne("CostEstimationApp.Models.MachineType", "MachineType")
                        .WithMany("Machine")
                        .HasForeignKey("MachineTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CostEstimationApp.Models.Worker", "Worker")
                        .WithMany("Machines")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MachineType");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("CostEstimationApp.Models.MRR", b =>
                {
                    b.HasOne("CostEstimationApp.Models.Material", "Material")
                        .WithMany("MRR")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CostEstimationApp.Models.ToolMaterial", "ToolMaterial")
                        .WithMany("MRR")
                        .HasForeignKey("ToolMaterialId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("ToolMaterial");
                });

            modelBuilder.Entity("CostEstimationApp.Models.Operation", b =>
                {
                    b.HasOne("CostEstimationApp.Models.Feature", "Feature")
                        .WithMany("Operations")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CostEstimationApp.Models.MRR", "MRR")
                        .WithMany("Operation")
                        .HasForeignKey("MRRId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CostEstimationApp.Models.Machine", "Machine")
                        .WithMany("Operations")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CostEstimationApp.Models.OperationSet", "OperationSet")
                        .WithMany("Operations")
                        .HasForeignKey("OperationSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CostEstimationApp.Models.OperationType", "OperationType")
                        .WithMany("Operations")
                        .HasForeignKey("OperationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Projekt", "Projekt")
                        .WithMany("Operations")
                        .HasForeignKey("ProjektId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CostEstimationApp.Models.SemiFinishedProduct", "SemiFinishedProduct")
                        .WithMany("Operation")
                        .HasForeignKey("SemiFinishedProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CostEstimationApp.Models.Tool", "Tool")
                        .WithMany("Operation")
                        .HasForeignKey("ToolId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Feature");

                    b.Navigation("MRR");

                    b.Navigation("Machine");

                    b.Navigation("OperationSet");

                    b.Navigation("OperationType");

                    b.Navigation("Projekt");

                    b.Navigation("SemiFinishedProduct");

                    b.Navigation("Tool");
                });

            modelBuilder.Entity("CostEstimationApp.Models.OperationSet", b =>
                {
                    b.HasOne("Projekt", "Projekt")
                        .WithMany("OperationSets")
                        .HasForeignKey("ProjektId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Projekt");
                });

            modelBuilder.Entity("CostEstimationApp.Models.SemiFinishedProduct", b =>
                {
                    b.HasOne("CostEstimationApp.Models.Material", "Material")
                        .WithMany("SemiFinishedProduct")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Material");
                });

            modelBuilder.Entity("CostEstimationApp.Models.Tool", b =>
                {
                    b.HasOne("CostEstimationApp.Models.ToolMaterial", "ToolMaterial")
                        .WithMany("Tools")
                        .HasForeignKey("ToolMaterialId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ToolMaterial");
                });

            modelBuilder.Entity("MachineOperationType", b =>
                {
                    b.HasOne("CostEstimationApp.Models.Machine", null)
                        .WithMany()
                        .HasForeignKey("MachinesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CostEstimationApp.Models.OperationType", null)
                        .WithMany()
                        .HasForeignKey("OperationTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OperationTypeTool", b =>
                {
                    b.HasOne("CostEstimationApp.Models.OperationType", null)
                        .WithMany()
                        .HasForeignKey("OperationTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CostEstimationApp.Models.Tool", null)
                        .WithMany()
                        .HasForeignKey("ToolsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Projekt", b =>
                {
                    b.HasOne("CostEstimationApp.Models.SemiFinishedProduct", "SemiFinishedProduct")
                        .WithMany("Projekts")
                        .HasForeignKey("SemiFinishedProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SemiFinishedProduct");
                });

            modelBuilder.Entity("Przedmiot", b =>
                {
                    b.HasOne("CostEstimationApp.Models.AccuracyClass", "AccuracyClass")
                        .WithMany("Przedmiots")
                        .HasForeignKey("AccuracyClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CostEstimationApp.Models.Feature", "Feature")
                        .WithMany("Przedmiots")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Projekt", "Projekt")
                        .WithMany("Przedmiots")
                        .HasForeignKey("ProjektId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccuracyClass");

                    b.Navigation("Feature");

                    b.Navigation("Projekt");
                });

            modelBuilder.Entity("CostEstimationApp.Models.AccuracyClass", b =>
                {
                    b.Navigation("Przedmiots");
                });

            modelBuilder.Entity("CostEstimationApp.Models.Feature", b =>
                {
                    b.Navigation("FeatureOperationTypes");

                    b.Navigation("Operations");

                    b.Navigation("Przedmiots");
                });

            modelBuilder.Entity("CostEstimationApp.Models.Machine", b =>
                {
                    b.Navigation("Operations");
                });

            modelBuilder.Entity("CostEstimationApp.Models.MachineType", b =>
                {
                    b.Navigation("Machine");
                });

            modelBuilder.Entity("CostEstimationApp.Models.Material", b =>
                {
                    b.Navigation("MRR");

                    b.Navigation("SemiFinishedProduct");
                });

            modelBuilder.Entity("CostEstimationApp.Models.MRR", b =>
                {
                    b.Navigation("Operation");
                });

            modelBuilder.Entity("CostEstimationApp.Models.OperationSet", b =>
                {
                    b.Navigation("Operations");
                });

            modelBuilder.Entity("CostEstimationApp.Models.OperationType", b =>
                {
                    b.Navigation("FeatureOperationTypes");

                    b.Navigation("Operations");
                });

            modelBuilder.Entity("CostEstimationApp.Models.SemiFinishedProduct", b =>
                {
                    b.Navigation("Operation");

                    b.Navigation("Projekts");
                });

            modelBuilder.Entity("CostEstimationApp.Models.Tool", b =>
                {
                    b.Navigation("Operation");
                });

            modelBuilder.Entity("CostEstimationApp.Models.ToolMaterial", b =>
                {
                    b.Navigation("MRR");

                    b.Navigation("Tools");
                });

            modelBuilder.Entity("CostEstimationApp.Models.Worker", b =>
                {
                    b.Navigation("Machines");
                });

            modelBuilder.Entity("Projekt", b =>
                {
                    b.Navigation("OperationSets");

                    b.Navigation("Operations");

                    b.Navigation("Przedmiots");
                });
#pragma warning restore 612, 618
        }
    }
}
