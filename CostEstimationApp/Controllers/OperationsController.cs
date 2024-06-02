using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CostEstimationApp.Data;
using CostEstimationApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CostEstimationApp.Controllers
{
    public class OperationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OperationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Operations
        public async Task<IActionResult> Index()
        {
            int? selectedOperationSetId = HttpContext.Session.GetInt32("SelectedOperationSetId");
            if (selectedOperationSetId == null)
            {
                return RedirectToAction("Index", "OperationSets");
            }

            var operations = await _context.Operations
                .Include(o => o.MRR)
                .Include(o => o.Machine)
                .Include(o => o.OperationType)
                .Include(o => o.SemiFinishedProduct)
                .Include(o => o.Tool)
                .Include(o => o.Worker)
                .Where(o => o.OperationSetId == selectedOperationSetId)
                .ToListAsync();

            return View(operations);
        }

        // GET: Operations/Create
        public async Task<IActionResult> Create()
        {
            int? selectedOperationSetId = HttpContext.Session.GetInt32("SelectedOperationSetId");
            if (selectedOperationSetId == null)
            {
                return RedirectToAction("Index", "OperationSets");
            }

            int? projectId = HttpContext.Session.GetInt32("SelectedProjectId");
            if (projectId == null)
            {
                return RedirectToAction("Index", "Projekts");
            }

            var projectFeatures = await _context.Projekts
                .Where(p => p.Id == projectId)
                .SelectMany(p => p.Przedmiots)
                .Select(p => p.Feature)
                .Distinct()
                .ToListAsync();

            ViewData["MRRId"] = new SelectList(_context.MRRs, "Id", "Id");
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name");
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name");
            ViewData["SemiFinishedProductId"] = new SelectList(_context.SemiFinishedProducts, "Id", "Id");
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Id");
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Id");
            ViewData["FeatureId"] = new SelectList(projectFeatures, "Id", "Name");
            return View();
        }

        // POST: Operations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SemiFinishedProductId,MachineId,WorkerId,ToolId,OperationTypeId,MRRId,CuttingLength,CuttingWidth,CuttingDepth,PocketLength,PocketWidth,PocketDepth,DrillDiameter,DrillDepth,FaceMillingDepth,FinishingMillingDepth,FaceArea,LengthBeforeOperation,WidthBeforeOperation,HeightBeforeOperation,LengthAfterOperation,WidthAfterOperation,HeightAfterOperation,VolumeToRemove,MachiningTime,FeatureId")] Operation operation)
        {
            int? projectId = HttpContext.Session.GetInt32("SelectedProjectId");
            if (projectId == null)
            {
                return RedirectToAction("Index", "Projekts");
            }

            int? selectedOperationSetId = HttpContext.Session.GetInt32("SelectedOperationSetId");
            if (selectedOperationSetId == null)
            {
                return RedirectToAction("Index", "OperationSets");
            }

            if (ModelState.IsValid)
            {
                operation.ProjektId = projectId.Value;
                operation.OperationSetId = selectedOperationSetId.Value;

                // Pobierz półfabrykat
                var semiFinishedProduct = await _context.SemiFinishedProducts
                    .Include(sp => sp.Material)
                    .FirstOrDefaultAsync(sp => sp.Id == operation.SemiFinishedProductId);
                if (semiFinishedProduct == null)
                {
                    return NotFound();
                }

                // Pobierz narzędzie
                var tool = await _context.Tools
                    .Include(t => t.ToolMaterial)
                    .FirstOrDefaultAsync(t => t.Id == operation.ToolId);
                if (tool == null)
                {
                    return NotFound();
                }

                // Pobierz materiał półfabrykatu
                var material = semiFinishedProduct.Material;
                if (material == null)
                {
                    return NotFound();
                }

                // Pobierz materiał narzędzia
                var toolMaterial = tool.ToolMaterial;
                if (toolMaterial == null)
                {
                    return NotFound();
                }

                // Znajdź MRR na podstawie ToolMaterialId i MaterialId
                var mrr = await _context.MRRs
                    .FirstOrDefaultAsync(m => m.ToolMaterialId == toolMaterial.Id && m.MaterialId == material.Id);
                if (mrr == null)
                {
                    return NotFound();
                }

                operation.MRRId = mrr.Id;

                // Pobierz wymiary z poprzedniej operacji, jeśli istnieje
                var previousOperation = await _context.Operations
                    .Where(o => o.SemiFinishedProductId == operation.SemiFinishedProductId)
                    .OrderByDescending(o => o.Id)
                    .FirstOrDefaultAsync();

                if (previousOperation != null)
                {
                    operation.LengthBeforeOperation = previousOperation.LengthAfterOperation;
                    operation.WidthBeforeOperation = previousOperation.WidthAfterOperation;
                    operation.HeightBeforeOperation = previousOperation.HeightAfterOperation;
                    operation.FaceArea = previousOperation.FaceArea;
                }
                else
                {
                    operation.LengthBeforeOperation = semiFinishedProduct.DimensionX;
                    operation.WidthBeforeOperation = semiFinishedProduct.DimensionY;
                    operation.HeightBeforeOperation = semiFinishedProduct.DimensionZ;
                    operation.FaceArea = semiFinishedProduct.DimensionX * semiFinishedProduct.DimensionY;
                }

                // Pobierz OperationType
                var operationType = await _context.OperationTypes
                    .FirstOrDefaultAsync(ot => ot.Id == operation.OperationTypeId);
                if (operationType == null)
                {
                    return NotFound();
                }
                operation.OperationType = operationType;

                // Obliczenia dla typu operacji
                if (operation.OperationType.Name == "Cutting")
                {
                    operation.LengthAfterOperation = operation.LengthBeforeOperation - operation.CuttingLength.GetValueOrDefault();
                    operation.WidthAfterOperation = operation.WidthBeforeOperation - operation.CuttingWidth.GetValueOrDefault();
                    operation.HeightAfterOperation = operation.HeightBeforeOperation - operation.CuttingDepth.GetValueOrDefault();
                    operation.VolumeToRemove = operation.CuttingLength.GetValueOrDefault() * operation.CuttingWidth.GetValueOrDefault() * operation.CuttingDepth.GetValueOrDefault();
                    operation.FaceArea = operation.FaceArea - operation.CuttingLength.GetValueOrDefault() * operation.CuttingWidth.GetValueOrDefault();
                }
                else if (operation.OperationType.Name == "Drilling")
                {
                    var radius = operation.DrillDiameter.GetValueOrDefault() / 2;
                    operation.VolumeToRemove = (decimal)Math.PI * radius * radius * operation.DrillDepth.GetValueOrDefault();
                    operation.LengthAfterOperation = operation.LengthBeforeOperation;
                    operation.WidthAfterOperation = operation.WidthBeforeOperation;
                    operation.HeightAfterOperation = operation.HeightBeforeOperation;
                    operation.FaceArea = operation.FaceArea - (decimal)Math.PI * radius * radius;
                }
                else if (operation.OperationType.Name == "Pocket Milling")
                {
                    operation.VolumeToRemove = operation.PocketLength.GetValueOrDefault() * operation.PocketWidth.GetValueOrDefault() * operation.PocketDepth.GetValueOrDefault();
                    operation.LengthAfterOperation = operation.LengthBeforeOperation;
                    operation.WidthAfterOperation = operation.WidthBeforeOperation;
                    operation.HeightAfterOperation = operation.HeightBeforeOperation;
                    operation.FaceArea = operation.FaceArea - operation.PocketLength.GetValueOrDefault() * operation.PocketWidth.GetValueOrDefault();
                }
                else if (operation.OperationType.Name == "Face Milling")
                {
                    operation.LengthAfterOperation = operation.LengthBeforeOperation;
                    operation.WidthAfterOperation = operation.WidthBeforeOperation;
                    operation.HeightAfterOperation = operation.HeightBeforeOperation - operation.FaceMillingDepth.GetValueOrDefault();
                    operation.VolumeToRemove = operation.LengthBeforeOperation * operation.WidthBeforeOperation * operation.FaceMillingDepth.GetValueOrDefault();
                }
                else if (operation.OperationType.Name == "Finishing Milling")
                {
                    operation.HeightAfterOperation = operation.HeightBeforeOperation - operation.FinishingMillingDepth.GetValueOrDefault();
                    operation.VolumeToRemove = operation.FinishingMillingDepth.GetValueOrDefault() * operation.FaceArea;
                }

                operation.MachiningTime = operation.VolumeToRemove / mrr.Rate;

                // Pobierz koszt maszyny, pracownika i narzędzia
                var machine = await _context.Machines
                    .Include(m => m.MachineType) // Pobierz powiązany MachineType
                    .FirstOrDefaultAsync(m => m.Id == operation.MachineId);
                if (machine == null || machine.MachineType == null)
                {
                    return NotFound();
                }

                var worker = await _context.Workers.FirstOrDefaultAsync(w => w.Id == operation.WorkerId);
                if (worker == null)
                {
                    return NotFound();
                }

                // Użyj AdditionalTime z MachineType
                operation.MachineCost = (machine.CostPerHour * operation.MachiningTime) * (1 + (decimal)machine.MachineType.AdditionalTime);
                operation.WorkerCost = (worker.CostPerHour * operation.MachiningTime) * (1 + (decimal)machine.MachineType.AdditionalTime + (decimal)machine.MachineType.AuxiliaryTime);
                operation.ToolCost = tool.CostPerHour * operation.MachiningTime;

                operation.TotalCost = operation.MachineCost + operation.WorkerCost + operation.ToolCost;

                _context.Add(operation);
                await _context.SaveChangesAsync();

                // Aktualizacja kosztów OperationSet
                await UpdateOperationSetCosts(operation.OperationSetId);

                return RedirectToAction(nameof(Index));
            }
            projectId = int.Parse(HttpContext.Session.GetString("SelectedProjectId"));
            var projectFeatures = await _context.Projekts
                .Where(p => p.Id == projectId)
                .SelectMany(p => p.Przedmiots)
                .Select(p => p.Feature)
                .Distinct()
                .ToListAsync();

            ViewData["MRRId"] = new SelectList(_context.MRRs, "Id", "Id", operation.MRRId);
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name", operation.MachineId);
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name", operation.OperationTypeId);
            ViewData["SemiFinishedProductId"] = new SelectList(_context.SemiFinishedProducts, "Id", "Id", operation.SemiFinishedProductId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Id", operation.ToolId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Id", operation.WorkerId);
            ViewData["FeatureId"] = new SelectList(projectFeatures, "Id", "Name", operation.FeatureId);
            return View(operation);
        }

        // GET: Operations/GetOperationTypesByFeature
        [HttpGet]
        public async Task<IActionResult> GetOperationTypesByFeature(int featureId)
        {
            Console.WriteLine($"Received Feature ID: {featureId}");

            var operationTypes = await _context.FeatureOperationTypes
                .Where(fot => fot.FeatureId == featureId)
                .Select(fot => fot.OperationType)
                .ToListAsync();

            Console.WriteLine($"Operation Types Count: {operationTypes.Count}");

            var operationTypeIds = operationTypes.Select(ot => ot.Id).ToList();

            var machines = await _context.Machines
                .Where(m => m.OperationTypes.Any(ot => operationTypeIds.Contains(ot.Id)))
                .ToListAsync();

            var tools = await _context.Tools
                .Where(t => t.OperationTypes.Any(ot => operationTypeIds.Contains(ot.Id)))
                .ToListAsync();

            Console.WriteLine($"Machines Count: {machines.Count}");
            Console.WriteLine($"Tools Count: {tools.Count}");

            return Json(new
            {
                operationTypes = operationTypes.Select(ot => new { id = ot.Id, name = ot.Name }),
                machines = machines.Select(m => new { id = m.Id, name = m.Name }),
                tools = tools.Select(t => new { id = t.Id, name = t.Name })
            });
        }

        // Metoda do aktualizacji kosztów OperationSet
        private async Task UpdateOperationSetCosts(int operationSetId)
        {
            var operationSet = await _context.OperationSets
                .Include(os => os.Operations)
                .FirstOrDefaultAsync(os => os.Id == operationSetId);

            if (operationSet != null)
            {
                operationSet.MachineCost = operationSet.Operations.Sum(o => o.MachineCost);
                operationSet.ToolCost = operationSet.Operations.Sum(o => o.ToolCost);
                operationSet.WorkerCost = operationSet.Operations.Sum(o => o.WorkerCost);
                operationSet.TotalCost = operationSet.MachineCost + operationSet.ToolCost + operationSet.WorkerCost;

                _context.Update(operationSet);
                await _context.SaveChangesAsync();
            }
        }
    }
}
