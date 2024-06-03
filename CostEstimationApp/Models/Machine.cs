using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class Machine
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CostPerHour { get; set; }

        [Required(ErrorMessage = "Machine type is required.")]
        [ForeignKey("MachineTypeId")]
        public int MachineTypeId { get; set; }
        public MachineType? MachineType { get; set; }

        [Required(ErrorMessage = "Machine type is required.")]
        [ForeignKey("WorkerId")]
        public int WorkerId { get; set; }
        public Worker? Worker { get; set; }

        public List<OperationType> OperationTypes { get; set; } = new List<OperationType>();
        public List<Operation> Operations { get; set; } = new List<Operation>();

    }
}