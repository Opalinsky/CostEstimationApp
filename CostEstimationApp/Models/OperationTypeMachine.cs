using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class OperationTypeMachine
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Operation type is required.")]
        [ForeignKey("OperationTypeId")]
        public int OperationTypeId { get; set; }
        public OperationType? OperationType { get; set; }
        
        [Required(ErrorMessage = "Operation type is required.")]
        [ForeignKey("MachineId")]
        public int MachineId { get; set; }
        public Machine? Machine { get; set; }


    }
}
