using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class FeatureOperationType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Feature")]
        public int FeatureId { get; set; }
        public Feature? Feature { get; set; }

        [Required]
        [ForeignKey("OperationType")]
        public int OperationTypeId { get; set; }
        public OperationType? OperationType { get; set; }
    }
}
