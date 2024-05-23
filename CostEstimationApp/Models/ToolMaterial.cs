using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class ToolMaterial
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public List<Tool> Tools { get; set; } = new List<Tool>();
        public List<MRR> MRR { get; set; } = new List<MRR>();

    }
}
