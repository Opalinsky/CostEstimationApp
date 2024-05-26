using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class OperationType
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public List<OperationTypeMachine> OperationTypeMachines { get; set; } = new List<OperationTypeMachine>();
        public List<OperationTypeTool> OperationTypeTools { get; set; } = new List<OperationTypeTool>();
        public List<Operation> Operations { get; set; } = new List<Operation>();
    }
}