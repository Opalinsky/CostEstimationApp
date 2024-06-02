using Microsoft.EntityFrameworkCore;
using CostEstimationApp.Models;

namespace CostEstimationApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Material> Materials { get; set; } = null!;
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Tool> Tools { get; set; } = null!;
        public DbSet<Worker> Workers { get; set; }
        public DbSet<SemiFinishedProduct> SemiFinishedProducts { get; set; }
        public DbSet<MRR> MRRs { get; set; } = null!;
        public DbSet<Operation> Operations { get; set; }
        public DbSet<MachineType> MachineTypes { get; set; }
        public DbSet<ToolMaterial> ToolMaterials { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<OperationSet> OperationSets { get; set; }
        public DbSet<Projekt> Projekts { get; set; }
        public DbSet<Przedmiot> Przedmiots { get; set; }
        public DbSet<Proces> Process { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<FeatureOperationType> FeatureOperationTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OperationType>()
                .HasMany(ot => ot.Machines)
                .WithMany(m => m.OperationTypes)
                .UsingEntity(j => j.ToTable("OperationTypeMachines"));

            modelBuilder.Entity<OperationType>()
                .HasMany(ot => ot.Tools)
                .WithMany(t => t.OperationTypes)
                .UsingEntity(j => j.ToTable("OperationTypeTools"));

            modelBuilder.Entity<FeatureOperationType>()
                 .HasOne(fot => fot.Feature)
                 .WithMany(f => f.FeatureOperationTypes)
                 .HasForeignKey(fot => fot.FeatureId);

            modelBuilder.Entity<FeatureOperationType>()
                .HasOne(fot => fot.OperationType)
                .WithMany(ot => ot.FeatureOperationTypes)
                .HasForeignKey(fot => fot.OperationTypeId);
            
            modelBuilder.Entity<Operation>()
                .HasOne(fot => fot.OperationType)
                .WithMany(ot => ot.Operations)
                .HasForeignKey(fot => fot.OperationTypeId);

            // Define the one-to-many relationship between Przedmiot and Feature
            modelBuilder.Entity<Przedmiot>()
                .HasOne(p => p.Feature)
                .WithMany(f => f.Przedmiots)
                .HasForeignKey(p => p.FeatureId);

            modelBuilder.Entity<Operation>()
              .HasOne(p => p.Feature)
              .WithMany(f => f.Operations)
              .HasForeignKey(p => p.FeatureId);


            // Konfiguracja unikalnego indeksu dla MRR
            modelBuilder.Entity<MRR>()
                .HasIndex(m => new { m.MaterialId, m.ToolMaterialId })
                .IsUnique();

            // Relacje dla MRR
            modelBuilder.Entity<ToolMaterial>()
                .HasMany(m => m.Tools)
                .WithOne(t => t.ToolMaterial)
                .HasForeignKey(m => m.ToolMaterialId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacje dla MRR
            modelBuilder.Entity<Material>()
                .HasMany(m => m.SemiFinishedProduct)
                .WithOne(t => t.Material)
                .HasForeignKey(m => m.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);

            // Konfiguracja relacji dla tabeli Operations
            modelBuilder.Entity<MachineType>()
                .HasMany(m => m.Machine)
                .WithOne(a => a.MachineType)
                .HasForeignKey(o => o.MachineTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Konfiguracja relacji dla tabeli Operations
            modelBuilder.Entity<Material>()
                .HasMany(m => m.MRR)
                .WithOne(a => a.Material)
                .HasForeignKey(o => o.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);

            // Konfiguracja relacji dla tabeli Operations
            modelBuilder.Entity<ToolMaterial>()
                .HasMany(b => b.MRR)
                .WithOne(a => a.ToolMaterial)
                .HasForeignKey(o => o.ToolMaterialId)
                .OnDelete(DeleteBehavior.Restrict);

            //Jeden MMR do wielu operacji 
            modelBuilder.Entity<MRR>()
                .HasMany(a => a.Operation)
                .WithOne(o => o.MRR)
                .HasForeignKey(o => o.MRRId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Projekt>()
                .HasMany(a => a.Operations)
                .WithOne(o => o.Projekt)
                .HasForeignKey(o => o.ProjektId)
                .OnDelete(DeleteBehavior.Restrict);

            //Jedna maszyna do wielu operacji
            modelBuilder.Entity<Machine>()
               .HasMany(a => a.Operations)
               .WithOne(o => o.Machine)
               .HasForeignKey(o => o.MachineId)
               .OnDelete(DeleteBehavior.Restrict);

            //Jeden półfabrykat do wielu operacji
            modelBuilder.Entity<SemiFinishedProduct>()
                .HasMany(a => a.Operation)
                .WithOne(o => o.SemiFinishedProduct)
                .HasForeignKey(o => o.SemiFinishedProductId)
                .OnDelete(DeleteBehavior.Restrict);

            //Jeden półfabrykat do wielu operacji
            modelBuilder.Entity<SemiFinishedProduct>()
                .HasMany(a => a.Projekts)
                .WithOne(o => o.SemiFinishedProduct)
                .HasForeignKey(o => o.SemiFinishedProductId)
                .OnDelete(DeleteBehavior.Restrict);

            //Jedno narzędzie do wielu operacji
            modelBuilder.Entity<Tool>()
                .HasMany(a => a.Operation)
                .WithOne(o => o.Tool)
                .HasForeignKey(o => o.ToolId)
                .OnDelete(DeleteBehavior.Restrict);

            //Jeden pracownik do wielu operacji 
            modelBuilder.Entity<Worker>()
                .HasMany(a => a.Operation)
                .WithOne(o => o.Worker)
                .HasForeignKey(o => o.WorkerId)
                .OnDelete(DeleteBehavior.Restrict);

            //Jeden OperationSet dla wielu operacji 
            modelBuilder.Entity<OperationSet>()
                .HasMany(a => a.Operations)
                .WithOne(o => o.OperationSet)
                .HasForeignKey(o => o.OperationSetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
