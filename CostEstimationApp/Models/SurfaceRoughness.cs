using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostEstimationApp.Models
{
    public class SurfaceRoughness
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Przedmiot> Przedmiots { get; set; } = new List<Przedmiot>();
    }
}

