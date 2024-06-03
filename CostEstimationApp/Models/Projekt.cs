using CostEstimationApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Projekt
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
   // public int Value { get; set; }
    public List<Przedmiot> Przedmiots { get; set; } = new List<Przedmiot>();

    [Required(ErrorMessage = "Półfabrykat is required.")]
    [ForeignKey("SemiFinishedProductId")]
    public int SemiFinishedProductId { get; set; }
    public SemiFinishedProduct? SemiFinishedProduct { get; set; }

    public List<OperationSet> OperationSets { get; set; } = new List<OperationSet>();
    public List<Operation> Operations { get; set; } = new List<Operation>();

}
