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

            var projectFeatures = await _context.Przedmiots
                .Where(p => p.ProjektId == projectId)
                .Select(p => p.Feature)
                .Distinct()
                .ToListAsync();

            ViewData["MRRId"] = new SelectList(_context.MRRs, "Id", "Id");
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name");
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name");
            ViewData["SemiFinishedProductId"] = new SelectList(_context.SemiFinishedProducts, "Id", "Id");
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Id");
            ViewData["FeatureId"] = new SelectList(projectFeatures, "Id", "Name");
            return View();
        }

        // POST: Operations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SemiFinishedProductId,MachineId,ToolId,OperationTypeId,MRRId,SetUpTime,CuttingLength,CuttingWidth,CuttingDepth,PocketLength,PocketWidth,PocketDepth,DrillDiameter,DrillDepth,FaceMillingDepth,FinishingMillingDepth,FaceArea,LengthBeforeOperation,WidthBeforeOperation,HeightBeforeOperation,LengthAfterOperation,WidthAfterOperation,HeightAfterOperation,VolumeToRemove,MachiningTime,FeatureId")] Operation operation)
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

                // Pobierz OperationType
                var operationType = await _context.OperationTypes
                    .FirstOrDefaultAsync(ot => ot.Id == operation.OperationTypeId);
                if (operationType == null)
                {
                    return NotFound();
                }
                operation.OperationType = operationType;

                // Pobierz Przedmiot dla operacji
                var przedmiot = await _context.Przedmiots
                    .Where(p => p.ProjektId == projectId && p.FeatureId == operation.FeatureId)
                    .FirstOrDefaultAsync();
                
                // Obliczenia dla typu operacji
                if (operation.OperationType.Name == "Frezowanie Zgrubne Płaszczyzny Górnej")
                {
                    Console.WriteLine($"volume to remove: {przedmiot.VolumeToRemove}");
                    operation.VolumeToRemove = przedmiot.VolumeToRemove;
                    var Rate = mrr.Rate * 1000;
                    operation.MachiningTime = operation.VolumeToRemove / Rate;
                    Console.WriteLine(mrr.Rate);
                    Console.WriteLine(operation.MachiningTime);
                }
                else if (operation.OperationType.Name == "Frezowanie Wykańczające Płaszczyzny")
                {
                    Console.WriteLine($"volume to remove finish: {przedmiot.VolumeToRemoveFinish}");
                    operation.VolumeToRemoveFinish = przedmiot.VolumeToRemoveFinish;
                    var RateFinish = mrr.RateFinish * 1000;
                    operation.MachiningTime = operation.VolumeToRemoveFinish / RateFinish;
                    Console.WriteLine($"mrr finish: {mrr.RateFinish}");
                    Console.WriteLine(operation.MachiningTime);
                }
                else if (operation.OperationType.Name == "Wiercenie Zgrubne")
                {
                    Console.WriteLine($"volume to remove: {przedmiot.VolumeToRemove}");
                    operation.VolumeToRemove = przedmiot.VolumeToRemove;
                    var Rate = mrr.Rate * 1000;
                    operation.MachiningTime = operation.VolumeToRemove / Rate;
                    Console.WriteLine(mrr.Rate);
                    Console.WriteLine(operation.MachiningTime);
                }
                else if (operation.OperationType.Name == "Wiercenie Wykańczające")
                {
                    Console.WriteLine($"volume to remove finish: {przedmiot.VolumeToRemoveFinish}");
                    operation.VolumeToRemoveFinish = przedmiot.VolumeToRemoveFinish;
                    var RateFinish = mrr.RateFinish * 1000;
                    operation.MachiningTime = operation.VolumeToRemoveFinish / RateFinish;
                    Console.WriteLine($"mrr finish: {mrr.RateFinish}");
                    Console.WriteLine(operation.MachiningTime);
                }
                else if (operation.OperationType.Name == "Rozwiercanie")
                {
                    Console.WriteLine($"volume to remove finish: {przedmiot.VolumeToRemoveFinish}");
                    operation.VolumeToRemoveFinish = przedmiot.VolumeToRemoveFinish;
                    var RateFinish = mrr.RateFinish * 1000;
                    operation.MachiningTime = operation.VolumeToRemoveFinish / RateFinish;
                    Console.WriteLine($"mrr finish: {mrr.RateFinish}");
                    Console.WriteLine(operation.MachiningTime);
                }
                else if (operation.OperationType.Name == "Frezowanie Zgrubne Kieszeni")
                {
                    Console.WriteLine($"volume to remove: {przedmiot.VolumeToRemove}");
                    operation.VolumeToRemove = przedmiot.VolumeToRemove;
                    var Rate = mrr.Rate * 1000;
                    operation.MachiningTime = operation.VolumeToRemove / Rate;
                    Console.WriteLine(mrr.Rate);
                    Console.WriteLine(operation.MachiningTime);
                }
                else if (operation.OperationType.Name == "Frezowanie Wykańczające Kieszeni")
                {
                    Console.WriteLine($"volume to remove finish: {przedmiot.VolumeToRemoveFinish}");
                    operation.VolumeToRemoveFinish = przedmiot.VolumeToRemoveFinish;
                    var RateFinish = mrr.RateFinish * 1000;
                    operation.MachiningTime = operation.VolumeToRemoveFinish / RateFinish;
                    Console.WriteLine($"mrr finish: {mrr.RateFinish}");
                    Console.WriteLine(operation.MachiningTime);
                }
                else if (operation.OperationType.Name == "Frezowanie Zgrubnie Rowka")
                {
                    Console.WriteLine($"volume to remove: {przedmiot.VolumeToRemove}");
                    operation.VolumeToRemove = przedmiot.VolumeToRemove;
                    var Rate = mrr.Rate * 1000;
                    operation.MachiningTime = operation.VolumeToRemove / Rate;
                    Console.WriteLine(mrr.Rate);
                    Console.WriteLine(operation.MachiningTime);

                }
                else if (operation.OperationType.Name == "Frezowanie Wykańczająco Rowka")
                {
                    Console.WriteLine($"volume to remove finish: {przedmiot.VolumeToRemoveFinish}");
                    operation.VolumeToRemoveFinish = przedmiot.VolumeToRemoveFinish;
                    var RateFinish = mrr.RateFinish * 1000;
                    operation.MachiningTime = operation.VolumeToRemoveFinish / RateFinish;
                    Console.WriteLine($"mrr finish: {mrr.RateFinish}");
                    Console.WriteLine(operation.MachiningTime);
                }
                else if (operation.OperationType.Name == "Frezowanie Zgrubnie Uskoku")
                {
                    Console.WriteLine($"volume to remove: {przedmiot.VolumeToRemove}");
                    operation.VolumeToRemove = przedmiot.VolumeToRemove;
                    var Rate = mrr.Rate * 1000;
                    operation.MachiningTime = operation.VolumeToRemove / Rate;
                    Console.WriteLine(mrr.Rate);
                    Console.WriteLine(operation.MachiningTime);
                }
                else if (operation.OperationType.Name == "Frezowanie Wykańczająco Uskoku")
                {
                    Console.WriteLine($"volume to remove finish: {przedmiot.VolumeToRemoveFinish}");
                    operation.VolumeToRemoveFinish = przedmiot.VolumeToRemoveFinish;
                    var RateFinish = mrr.RateFinish * 1000;
                    operation.MachiningTime = operation.VolumeToRemoveFinish / RateFinish;
                    Console.WriteLine($"mrr finish: {mrr.RateFinish}");
                    Console.WriteLine(operation.MachiningTime);
                }

                //operation.MachiningTime = operation.VolumeToRemove / mrr.Rate;
                Console.WriteLine($"Machining time is: {operation.MachiningTime}");

                // Pobierz koszt maszyny, pracownika i narzędzia
                var machine = await _context.Machines
                    .Include(m => m.MachineType) // Pobierz powiązany MachineType
                    .Include(m => m.Worker) // Pobierz powiązanego operatora
                    .FirstOrDefaultAsync(m => m.Id == operation.MachineId);
                if (machine == null || machine.MachineType == null || machine.Worker == null)
                {
                    return NotFound();
                }

                var worker = machine.Worker; // Pobierz operatora powiązanego z maszyną
                if (worker == null)
                {
                    return NotFound();
                }
                Console.WriteLine($"Worker Id is: {worker.Id}");

                // Użyj AdditionalTime z MachineType
                operation.MachineCost = ((machine.CostPerHour * operation.MachiningTime) * (1 + (decimal)machine.MachineType.AdditionalTime))/60;

                operation.WorkerCost = ((worker.CostPerHour * operation.MachiningTime) * (1 + (decimal)machine.MachineType.AdditionalTime + (decimal)machine.MachineType.AuxiliaryTime) + (worker.CostPerHour * (operation.SetUpTime.GetValueOrDefault())))/60;

                operation.ToolCost = (tool.CostPerHour * operation.MachiningTime)/60;

                Console.WriteLine($"Machine cost is: {operation.MachineCost}");
                Console.WriteLine($"Worker cost is: {operation.WorkerCost}");
                Console.WriteLine($"Tool cost is: {operation.ToolCost}");

                operation.TotalCost = operation.MachineCost + operation.WorkerCost + operation.ToolCost;
                Console.WriteLine($"Total cost is: {operation.TotalCost}");

                _context.Add(operation);
                await _context.SaveChangesAsync();

                // Aktualizacja kosztów OperationSet
                await UpdateOperationSetCosts(operation.OperationSetId);

                return RedirectToAction(nameof(Index));
            }

            var projectFeatures = await _context.Przedmiots
                .Where(p => p.ProjektId == projectId)
                .Select(p => p.Feature)
                .Distinct()
                .ToListAsync();

            ViewData["MRRId"] = new SelectList(_context.MRRs, "Id", "Id", operation.MRRId);
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name", operation.MachineId);
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name", operation.OperationTypeId);
            ViewData["SemiFinishedProductId"] = new SelectList(_context.SemiFinishedProducts, "Id", "Id", operation.SemiFinishedProductId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Id", operation.ToolId);
            ViewData["FeatureId"] = new SelectList(projectFeatures, "Id", "Name", operation.FeatureId);
            return View(operation);
        }

        // GET: Operations/GetOperationTypesByFeature
        [HttpGet]
        public async Task<IActionResult> GetOperationTypesByFeature(int featureId)
        {
            var operationTypes = await _context.FeatureOperationTypes
                .Where(fot => fot.FeatureId == featureId)
                .Select(fot => fot.OperationType)
                .ToListAsync();

            return Json(new
            {
                operationTypes = operationTypes.Select(ot => new { id = ot.Id, name = ot.Name })
            });
        }

        // GET: Operations/GetMachinesAndToolsByOperationType
        [HttpGet]
        public async Task<IActionResult> GetMachinesAndToolsByOperationType(int operationTypeId)
        {
            var machines = await _context.Machines
                .Where(m => m.OperationTypes.Any(ot => ot.Id == operationTypeId))
                .ToListAsync();

            var tools = await _context.Tools
                .Where(t => t.OperationTypes.Any(ot => ot.Id == operationTypeId))
                .ToListAsync();

            return Json(new
            {
                machines = machines.Select(m => new { id = m.Id, name = m.Name }),
                tools = tools.Select(t => new { id = t.Id, name = t.Name })
            });
        }

        // GET: Operations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operation = await _context.Operations
                .Include(o => o.MRR)
                .Include(o => o.Machine)
                .Include(o => o.OperationType)
                .Include(o => o.SemiFinishedProduct)
                .Include(o => o.Tool)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (operation == null)
            {
                return NotFound();
            }

            return View(operation);
        }

        // GET: Operations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operation = await _context.Operations
                .Include(o => o.MRR)
                .Include(o => o.Machine)
                .Include(o => o.OperationType)
                .Include(o => o.SemiFinishedProduct)
                .Include(o => o.Tool)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (operation == null)
            {
                return NotFound();
            }

            return View(operation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var operation = await _context.Operations.FindAsync(id);
                if (operation == null)
                {
                    return NotFound();
                }

                _context.Operations.Remove(operation);
                await _context.SaveChangesAsync();

                // Aktualizacja kosztów OperationSet po usunięciu operacji
                await UpdateOperationSetCosts(operation.OperationSetId);
            }
            catch (DbUpdateConcurrencyException)
            {
                
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        // Metoda do aktualizacji kosztów OperationSet
        private async Task UpdateOperationSetCosts(int operationSetId)
        {
            var operationSet = await _context.OperationSets
                .Include(os => os.Operations)
                .Include(os => os.Projekt)
                .FirstOrDefaultAsync(os => os.Id == operationSetId);

            if (operationSet != null)
            {
                operationSet.MachineCost = operationSet.Operations.Sum(o => o.MachineCost);
                operationSet.ToolCost = operationSet.Operations.Sum(o => o.ToolCost);
                operationSet.WorkerCost = operationSet.Operations.Sum(o => o.WorkerCost);
                operationSet.TotalCost = operationSet.MachineCost + operationSet.ToolCost + operationSet.WorkerCost;

                _context.Update(operationSet);
                await _context.SaveChangesAsync();
                await UpdateProjectCosts(operationSet.ProjektId);

            }
        }

        private async Task UpdateProjectCosts(int projektId)
        {
            var projekt = await _context.Projekts
                .Include(p => p.OperationSets)
                .FirstOrDefaultAsync(p => p.Id == projektId);

            if (projekt != null)
            {
                projekt.TotalCost = projekt.OperationSets.Sum(os => os.TotalCost) * projekt.Quantity;

                _context.Update(projekt);
                await _context.SaveChangesAsync();
            }
        }

    }
}