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

        [Column(TypeName = "decimal(18, 2)")]
        public decimal MachineCost { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ToolCost { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal WorkerCost { get; set; }

        [Column(TypeName = "decimal(18, 12)")]
        public decimal TotalCost { get; set; } 

        [ForeignKey("ProjektId")]
        public int ProjektId { get; set; }
        public Projekt? Projekt { get; set; }
        public List<Operation> Operations { get; set; } = new List<Operation>();
    }
}
