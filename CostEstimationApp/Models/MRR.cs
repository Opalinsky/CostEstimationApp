using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    [Index(nameof(MaterialId), nameof(ToolMaterialId), IsUnique = true)]
    public class MRR
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Material is required.")]
        [ForeignKey("MaterialId")]
        public int MaterialId { get; set; }
        public Material? Material { get; set; }
        
        [Required(ErrorMessage = "ToolMaterial is required.")]
        [ForeignKey("ToolMaterialId")]

        public int ToolMaterialId { get; set; }
        public ToolMaterial? ToolMaterial { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a positive number.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rate { get; set; }
        public List<Operation> Operation { get; set; } = new List<Operation>();

    }
}
