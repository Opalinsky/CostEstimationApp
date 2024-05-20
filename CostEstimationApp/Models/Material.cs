using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class Material
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PricePerKg { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Density { get; set; }

        public List<MRR> MRR { get; set; }

    }
}
