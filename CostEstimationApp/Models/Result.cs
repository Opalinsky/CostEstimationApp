using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MaterialCost { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OperationCost { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalCost { get; set; }
    }
}
