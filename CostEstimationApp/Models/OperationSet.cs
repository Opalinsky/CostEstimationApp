using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class OperationSet
    {
        [Key]     
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Ilość sztuk w serii musi być większa niż zero.")]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18, 12)")]
        public decimal PreparationTime { get; set; } // Czas przygotowawczo-zakończeniowy

        //[Required(ErrorMessage = "Worker type is required.")]
        //[ForeignKey("WorkerId")]
        //public int WorkerId { get; set; }
        //public Worker? Worker { get; set; }

        [Column(TypeName = "decimal(18, 12)")]
        public decimal TotalCost { get; set; } // Koszt całkowity

        [Column(TypeName = "decimal(18, 2)")]
        public decimal MachineCost { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ToolCost { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal WorkerCost { get; set; }

        public List<Operation> Operations { get; set; } = new List<Operation>();
    }
}
