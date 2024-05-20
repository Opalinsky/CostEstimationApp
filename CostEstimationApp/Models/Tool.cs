﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class Tool
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CostPerHour { get; set; }
        public List<MRR> MRR { get; set; }

    }
}
