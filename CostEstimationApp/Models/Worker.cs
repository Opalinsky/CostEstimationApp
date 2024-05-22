using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CostPerHour { get; set; }
        public List<Operation> Operation { get; set; } = new List<Operation>();

    }
}
