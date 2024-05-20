using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int MachineId { get; set; }
        public Machine Machine { get; set; }
        public int ToolId { get; set; }
        public Tool Tool { get; set; }
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
        public int MRRId { get; set; }
        public MRR MRR { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DimensionX { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DimensionY { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DimensionZ { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Duration { get; set; }
    }
}
