using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class OperationTypeTool
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Operation type is required.")]
        [ForeignKey("OperationTypeId")]
        public int OperationTypeId { get; set; }
        public OperationType? OperationType { get; set; }

        [Required(ErrorMessage = "Operation type is required.")]
        [ForeignKey("ToolId")]
        public int ToolId { get; set; }
        public Tool? Tool { get; set; }

    }
}
