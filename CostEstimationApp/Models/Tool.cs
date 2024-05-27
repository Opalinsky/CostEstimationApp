using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace CostEstimationApp.Models
{
    public class Tool
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int AmountOfEdges { get; set; }
        public decimal VitalityPerEdge { get; set; }

        public decimal CostPerHour { get; set; }
        public List<Operation> Operation { get; set; } = new List<Operation>();
        public List<OperationType> OperationTypes { get; set; } = new List<OperationType>();


        [Required(ErrorMessage = "Material is required.")]
        [ForeignKey("ToolMaterial")]
        public int ToolMaterialId { get; set; }
        public ToolMaterial? ToolMaterial { get; set; }
    }
}
