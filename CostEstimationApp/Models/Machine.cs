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

        //[Required(ErrorMessage = "Operation type is required.")]
        //[ForeignKey("OperationTypeId")]
        //public int OperationTypeId { get; set; }
        //public OperationType? OperationType { get; set; }
        public List<OperationTypeMachine> OperationTypeMachines { get; set; } = new List<OperationTypeMachine>();
        public List<Operation> Operation { get; set; } = new List<Operation>();


    }
}
