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
        public DbSet<OperationTypeMachine> OperationTypeMachines { get; set; }
        public DbSet<OperationTypeTool> OperationTypeTools { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<OperationType>().HasData(
                new OperationType { Id = 1, Name = "Cutting" },
                new OperationType { Id = 2, Name = "Drilling" }
            );

            modelBuilder.Entity<Machine>()
                .HasMany(m => m.OperationTypeMachines)
                .WithMany(a => a.Machines);

            modelBuilder.Entity<Tool>()
                .HasMany(m => m.OperationTypeTools)
                .WithMany(a => a.Tools);
            
            modelBuilder.Entity<OperationType>()
                .HasMany(m => m.OperationTypeMachines)
                .WithOne(t => t.OperationType)
                .HasForeignKey(m => m.OperationTypeId)
                .OnDelete(DeleteBehavior.Restrict);
           
            modelBuilder.Entity<OperationType>()
                .HasMany(m => m.OperationTypeTools)
                .WithOne(t => t.OperationType)
                .HasForeignKey(m => m.OperationTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Konfiguracja unikalnego indeksu dla MRR
            modelBuilder.Entity<MRR>()
                .HasIndex(m => new { m.MaterialId, m.ToolMaterialId })
                .IsUnique();

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


            //Jedna maszyna do wielu operacji
            modelBuilder.Entity<Machine>()
               .HasMany(a => a.Operation)
               .WithOne(o => o.Machine)
               .HasForeignKey(o => o.MachineId)
               .OnDelete(DeleteBehavior.Restrict);

            //Jeden półfabrykat do wielu operacji
            modelBuilder.Entity<SemiFinishedProduct>()
                .HasMany(a => a.Operation)
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
        }
    }
}
