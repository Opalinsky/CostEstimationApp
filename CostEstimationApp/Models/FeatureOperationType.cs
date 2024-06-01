using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class FeatureOperationType
    {
        public int Id { get; set; }

        [ForeignKey("Feature")]
        public int? FeatureId { get; set; }
        public Feature? Feature { get; set; } = null!;

        [ForeignKey("OperationType")]
        public int? OperationTypeId { get; set; }
        public OperationType? OperationType { get; set; } = null!;
    }
}
