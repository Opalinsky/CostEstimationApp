using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class FeatureOperationType
    {
        public int Id { get; set; }

        [ForeignKey("FeatureId")]
        public int FeatureId { get; set; }
        public Feature? Feature { get; set; } 

        [ForeignKey("OperationTypeId")]
        public int OperationTypeId { get; set; }
        public OperationType? OperationType { get; set; }
    }
}
