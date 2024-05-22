using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class MRR
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Material is required.")]
        [ForeignKey("MaterialId")]
        public int MaterialId { get; set; }
        public Material? Material { get; set; }
        


        [Required(ErrorMessage = "Tool is required.")]
        [ForeignKey("ToolId")]

        public int ToolId { get; set; }
        public Tool? Tool { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a positive number.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rate { get; set; }
        public List<Operation> Operation { get; set; } = new List<Operation>();

    }
}
