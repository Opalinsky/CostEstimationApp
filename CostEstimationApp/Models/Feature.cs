using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Przedmiot> Przedmiots { get; set; } = new List<Przedmiot>();
        public List<FeatureOperationType> FeatureOperationTypes { get; set; } = new List<FeatureOperationType>();
        public List<Operation> Operations { get; set; } = new List<Operation>();

    }
}

