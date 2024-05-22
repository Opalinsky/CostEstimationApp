using System.ComponentModel.DataAnnotations;

namespace CostEstimationApp.Models
{
    public class OperationType
    {
        public int Id { get; set; }
        public List<Operation> Operation { get; set; } = new List<Operation>();

        [Required(ErrorMessage = "OperationType type is required.")]
        public string Typeof { get; set; }
    }
}
