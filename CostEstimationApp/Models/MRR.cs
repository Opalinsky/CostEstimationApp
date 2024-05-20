using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class MRR
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Material is required.")]
        public int MaterialId { get; set; }

        [ForeignKey("MaterialId")]
        public Material Material { get; set; } = null!;
        


        [Required(ErrorMessage = "Tool is required.")]
        public int ToolId { get; set; }
       
        [ForeignKey("ToolId")]
        public Tool Tool { get; set; } = null!;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a positive number.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rate { get; set; }
    }
}
