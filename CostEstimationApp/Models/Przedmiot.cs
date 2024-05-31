﻿using CostEstimationApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

public class Przedmiot
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Required(ErrorMessage = "Projekt is required.")]
    [ForeignKey("ProjektId")]
    public int ProjektId { get; set; }
    public Projekt? Projekt { get; set; }
    public List<Proces> Process { get; set; } = new List<Proces>();
    //public List<OperationType> OperationTypes { get; set; } = new List<OperationType>();

    [Required(ErrorMessage = "Projekt is required.")]
    [ForeignKey("FeatureId")]
    public int FeatureId { get; set; }
    public Feature? Feature { get; set; }

    // Pola opcjonalne dla cech
    //public decimal? CuttingLength { get; set; }
    //public decimal? CuttingWidth { get; set; }
    //public decimal? CuttingDepth { get; set; }
    public decimal? DrillDiameter { get; set; }
    public decimal? DrillDepth { get; set; }
    public decimal? DrillApplicationCount { get; set; }

    public decimal? FaceMillingDepth { get; set; }
    public decimal? FinishingMillingDepth { get; set; }
    public decimal? AddFinishingMilling { get; set; }

    public decimal? PocketLength { get; set; }
    public decimal? PocketWidth { get; set; }
    public decimal? PocketDepth { get; set; }
    public decimal? AddFinishingOperation { get; set; }

    public decimal? SlotHeight { get; set; }
    public decimal? WhichSurface { get; set; }
    public decimal? SlotApplicationCount { get; set; }

    //https://procestechnologiczny.com.pl/frezowanie-podstawy-podzial-definicje/
    //Opcje frezowania
}