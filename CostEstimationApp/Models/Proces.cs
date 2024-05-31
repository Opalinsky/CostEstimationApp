using CostEstimationApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

public class Proces
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<OperationSet> OperationSets { get; set; } = new List<OperationSet>();
    
    [Column(TypeName = "decimal(18, 12)")]

    public decimal PreparationTime { get; set; }

    [Required(ErrorMessage = "Worker type is required.")]
    [ForeignKey("WorkerId")]
    public int WorkerId { get; set; }
    public Worker? Worker { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal MachineCost { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal ToolCost { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal WorkerCost { get; set; }

    [Column(TypeName = "decimal(18, 12)")]
    public decimal TotalCost { get; set; } // Koszt całkowity


}
