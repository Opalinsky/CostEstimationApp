using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class MachineType
    {
        public int Id { get; set; }
        public List<Machine> Machine { get; set; } = new List<Machine>();

        [Required(ErrorMessage = "Machine type is required.")]
        public string Typeof { get; set; }

        [Required(ErrorMessage = "Additional time is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Additional time must be a positive number.")]
        public double AdditionalTime { get; set; }

        [Required(ErrorMessage = "Auxiliary time is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Additional time must be a positive number.")]
        public double AuxiliaryTime { get; set; }

    }
}
