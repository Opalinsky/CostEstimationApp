using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class Feature
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Przedmiot> Przedmiot { get; set; } = new List<Przedmiot>();
        public List<OperationType> OperationTypes { get; set; } = new List<OperationType>();

    }
}

