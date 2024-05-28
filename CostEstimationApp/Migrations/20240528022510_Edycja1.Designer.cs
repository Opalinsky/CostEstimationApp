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
    [Migration("20240528022510_Edycja1")]
    partial class Edycja1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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

                    b.HasKey("Id");

                    b.HasIndex("MachineTypeId");

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

                    b.Property<int?>("OperationSetId")
                        .HasColumnType("int");

                    b.Property<int>("OperationTypeId")
                        .HasColumnType("int");

                    b.Property<int>("SemiFinishedProductId")
                        .HasColumnType("int");

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

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MRRId");

                    b.HasIndex("MachineId");

                    b.HasIndex("OperationSetId");

                    b.HasIndex("OperationTypeId");

                    b.HasIndex("SemiFinishedProductId");

                    b.HasIndex("ToolId");

                    b.HasIndex("WorkerId");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("CostEstimationApp.Models.OperationSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PreparationTime")
                        .HasColumnType("decimal(18,12)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(18,12)");

                    b.HasKey("Id");

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
                });

            modelBuilder.Entity("CostEstimationApp.Models.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("CostPerHour")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Workers");
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

            modelBuilder.Entity("CostEstimationApp.Models.Machine", b =>
                {
                    b.HasOne("CostEstimationApp.Models.MachineType", "MachineType")
                        .WithMany("Machine")
                        .HasForeignKey("MachineTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MachineType");
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
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostEstimationApp.Models.OperationType", "OperationType")
                        .WithMany("Operations")
                        .HasForeignKey("OperationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
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

                    b.HasOne("CostEstimationApp.Models.Worker", "Worker")
                        .WithMany("Operation")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MRR");

                    b.Navigation("Machine");

                    b.Navigation("OperationSet");

                    b.Navigation("OperationType");

                    b.Navigation("SemiFinishedProduct");

                    b.Navigation("Tool");

                    b.Navigation("Worker");
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
                    b.Navigation("Operations");
                });

            modelBuilder.Entity("CostEstimationApp.Models.SemiFinishedProduct", b =>
                {
                    b.Navigation("Operation");
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
                    b.Navigation("Operation");
                });
#pragma warning restore 612, 618
        }
    }
}
