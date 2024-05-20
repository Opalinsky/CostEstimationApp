using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class SemiFinishedProduct
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Material is required.")]
        [ForeignKey("MaterialId")]
        public int MaterialId { get; set; }
        public Material? Material { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DimensionX { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DimensionY { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DimensionZ { get; set; }
    }
}
