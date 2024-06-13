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
        public DbSet<Feature> Features { get; set; }
        public DbSet<FeatureOperationType> FeatureOperationTypes { get; set; }
        public DbSet<AccuracyClass> AccuracyClasses { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MachineType>().HasData(
             new MachineType { Id = 1, Typeof = "Automatyczna", AdditionalTime = 0.2, AuxiliaryTime = 0.1 },
             new MachineType { Id = 2, Typeof = "Konwencjonalna", AdditionalTime = 0.1, AuxiliaryTime = 0.2 }
            ); 

            //modelBuilder.Entity<Material>().HasData(
            //    new Material { Id = 1, Name = "Stal konstrukcyjna S275JR", PricePerKg = 2.40m, Density = 7850 },
            //    new Material { Id = 2, Name = "Stal stopowa 42CrMo4", PricePerKg = 9.5m, Density = 7700 }
            //);

            modelBuilder.Entity<ToolMaterial>().HasData(
                new ToolMaterial { Id = 1, Name = "Stal Szybkotnąca" }
            );

            modelBuilder.Entity<Worker>().HasData(
                new Worker { Id = 1, Name = "Pracownika1", CostPerHour = 50 }
            );

            modelBuilder.Entity<AccuracyClass>().HasData(
                new AccuracyClass { Id = 1, Name = "IT1" },
                new AccuracyClass { Id = 2, Name = "IT2" },
                new AccuracyClass { Id = 3, Name = "IT3" },
                new AccuracyClass { Id = 4, Name = "IT4" },
                new AccuracyClass { Id = 5, Name = "IT5" },
                new AccuracyClass { Id = 6, Name = "IT6" },
                new AccuracyClass { Id = 7, Name = "IT7" },
                new AccuracyClass { Id = 8, Name = "IT8" },
                new AccuracyClass { Id = 9, Name = "IT9" },
                new AccuracyClass { Id = 10, Name = "IT10" },
                new AccuracyClass { Id = 11, Name = "IT11" },
                new AccuracyClass { Id = 12, Name = "IT12" },
                new AccuracyClass { Id = 13, Name = "IT13" }
            );

            modelBuilder.Entity<OperationType>().HasData(
                new OperationType { Id = 1, Name = "Frezowanie Zgrubne Płaszczyzny Górnej" },
                new OperationType { Id = 2, Name = "Frezowanie Wykańczające Płaszczyzny" },
                new OperationType { Id = 3, Name = "Wiercenie Zgrubne" },
                new OperationType { Id = 4, Name = "Wiercenie Wykańczające" },
                new OperationType { Id = 5, Name = "Rozwiercanie" },
                new OperationType { Id = 6, Name = "Frezowanie Zgrubne Kieszeni" },
                new OperationType { Id = 7, Name = "Frezowanie Wykańczające Kieszeni" },
                new OperationType { Id = 8, Name = "Frezowanie Zgrubnie Rowka" },
                new OperationType { Id = 9, Name = "Frezowanie Wykańczająco Rowka" },
                new OperationType { Id = 10, Name = "Frezowanie Zgrubnie Uskoku" },
                new OperationType { Id = 11, Name = "Frezowanie Wykańczająco Uskoku" }
            );

            modelBuilder.Entity<Feature>().HasData(
                new Feature { Id = 1, Name = "Płaszczyzna Górna" },
                new Feature { Id = 2, Name = "Otwór" },
                new Feature { Id = 3, Name = "Kieszeń Zamknięta" },
                new Feature { Id = 4, Name = "Rowek Przelotowy" },
                new Feature { Id = 5, Name = "Uskok" }
            );




            modelBuilder.Entity<Przedmiot>()
                .HasOne(p => p.AccuracyClass)
                .WithMany(ac => ac.Przedmiots)
                .HasForeignKey(p => p.AccuracyClassId);

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
        
            modelBuilder.Entity<Projekt>()
                .HasMany(p => p.OperationSets)
                .WithOne(os => os.Projekt)
                .OnDelete(DeleteBehavior.Cascade);
        

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
                .HasMany(a => a.Machines)
                .WithOne(o => o.Worker)
                .HasForeignKey(o => o.WorkerId)
                .OnDelete(DeleteBehavior.Restrict);

            //Jeden OperationSet dla wielu operacji 
            modelBuilder.Entity<OperationSet>()
                .HasMany(a => a.Operations)
                .WithOne(o => o.OperationSet)
                .HasForeignKey(o => o.OperationSetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        
    }
}
