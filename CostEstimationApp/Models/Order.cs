using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int SemiFinishedProductId { get; set; }
        public SemiFinishedProduct SemiFinishedProduct { get; set; }
        public string Series { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PreLength { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PreWidth { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PreHeight { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PostLength { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PostWidth { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PostHeight { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Cost { get; set; }
    }
}
