using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CostEstimationApp.Models
{
    public class OperationType
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public List<FeatureOperationType> FeatureOperationTypes { get; set; } = new List<FeatureOperationType>();
        public List<Machine> Machines { get; set; } = new List<Machine> ();
        public List<Tool> Tools { get; set; } = new List<Tool> ();
        public List<Operation> Operations { get; set; } = new List <Operation> ();

    }
}