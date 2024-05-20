using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class MRR
    {
        public int Id { get; set; }

        [Required]
        public Material Material { get; set; } = null!;
        public int MaterialId { get; set; }


        [Required]
        public Tool Tool { get; set; } = null!;
        public int ToolId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a positive number.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rate { get; set; }
    }
}
