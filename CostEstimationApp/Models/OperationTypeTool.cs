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
        public List<Tool> Tools { get; set; } = new List<Tool>();

    }
}
