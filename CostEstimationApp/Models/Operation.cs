using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostEstimationApp.Models
{
    public class Operation
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        [Required(ErrorMessage = "SemiFinishedProduct type is required.")]
        [ForeignKey("SemiFinishedProductId")]
        public int SemiFinishedProductId { get; set; }
        public SemiFinishedProduct? SemiFinishedProduct { get; set; }

        [Required(ErrorMessage = "Machine type is required.")]
        [ForeignKey("MachineId")]
        public int MachineId { get; set; }
        public Machine? Machine { get; set; }

        [ForeignKey("ProjektId")]
        public int ProjektId { get; set; }
        public Projekt? Projekt { get; set; }

        [Required(ErrorMessage = "Machine type is required.")]
        [ForeignKey("FeatureId")]
        public int FeatureId { get; set; }
        public Feature? Feature { get; set; }

        [Required(ErrorMessage = "Tool type is required.")]
        [ForeignKey("ToolId")]
        public int ToolId { get; set; }
        public Tool? Tool { get; set; }
        
        [Required(ErrorMessage = "Operation type is required.")]
        [ForeignKey("OperationTypeId")]
        public int OperationTypeId { get; set; }
        public OperationType? OperationType { get; set; }

        [Required(ErrorMessage = "MRR is required.")]
        [ForeignKey("MRRId")]
        public int MRRId { get; set; }
        public MRR? MRR { get; set; }

        [ForeignKey("OperationSet")]
        public int OperationSetId { get; set; }
        public OperationSet? OperationSet { get; set; }

        public decimal? SetUpTime  { get; set; }
        // Fields for CuttingOperation
        public decimal? CuttingLength { get; set; }
        public decimal? CuttingWidth { get; set; }
        public decimal? CuttingDepth { get; set; }

        // Fields for DrillingOperation
        public decimal? DrillDiameter { get; set; }
        public decimal? DrillDepth { get; set; }

        public decimal? FinishingMillingDepth { get; set; }
        public decimal FaceArea { get; set; }
        public decimal? FaceMillingDepth { get; set; }

        public decimal? PocketLength { get; set; }
        public decimal? PocketWidth { get; set; }
        public decimal? PocketDepth { get; set; }

        public decimal LengthBeforeOperation { get; set; }
        public decimal WidthBeforeOperation { get; set; }
        public decimal HeightBeforeOperation { get; set; }

        public decimal LengthAfterOperation { get; set; }
        public decimal WidthAfterOperation { get; set; }
        public decimal HeightAfterOperation { get; set; }

        public decimal VolumeToRemove { get; set; }
        public decimal MachiningTime { get; set; }

        public decimal MachineCost  { get; set; }
        public decimal ToolCost { get;set; }
        public decimal WorkerCost { get;set; }
        public decimal TotalCost { get;set; }

    }
}
