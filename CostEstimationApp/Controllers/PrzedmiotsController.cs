using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CostEstimationApp.Data;
using CostEstimationApp.Models;
using Microsoft.AspNetCore.Http;

namespace CostEstimationApp.Controllers
{
    public class PrzedmiotsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrzedmiotsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Przedmiots
        public async Task<IActionResult> Index()
        {
            int? selectedProjectId = HttpContext.Session.GetInt32("SelectedProjectId");
            if (selectedProjectId == null)
            {
                return RedirectToAction("Index", "Projekts");
            }

            var applicationDbContext = _context.Przedmiots
                .Include(p => p.Feature)
                .Include(p => p.Projekt)
                .Include(p => p.AccuracyClass)
                .Include(p => p.SurfaceRoughness)
                .Include(p => p.FinishingAccuracyClass)
                .Include(p => p.FinishingSurfaceRoughness)
                .Where(p => p.ProjektId == selectedProjectId);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Przedmiots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Przedmiots == null)
            {
                return NotFound();
            }

            var przedmiot = await _context.Przedmiots
                .Include(p => p.Feature)
                .Include(p => p.Projekt)
                .Include(p => p.AccuracyClass)
                .Include(p => p.SurfaceRoughness)
                .Include(p => p.FinishingAccuracyClass)
                .Include(p => p.FinishingSurfaceRoughness)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (przedmiot == null)
            {
                return NotFound();
            }

            return View(przedmiot);
        }

        // GET: Przedmiots/Create
        public IActionResult Create()
        {
            int? selectedProjectId = HttpContext.Session.GetInt32("SelectedProjectId");
            if (selectedProjectId == null)
            {
                return RedirectToAction("Index", "Projekts");
            }

            ViewData["FeatureId"] = new SelectList(_context.Features, "Id", "Name");
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "Id", "Id", selectedProjectId);
            ViewData["AccuracyClassId"] = new SelectList(_context.AccuracyClasses, "Id", "Name");
            ViewData["SurfaceRoughnessId"] = new SelectList(_context.SurfaceRoughnesses, "Id", "Name");
            ViewData["FinishingAccuracyClassId"] = new SelectList(_context.FinishingAccuracyClasses, "Id", "Name");
            ViewData["FinishingSurfaceRoughnessId"] = new SelectList(_context.FinishingSurfaceRoughnesses, "Id", "Name");
            return View();
        }

        // POST: Przedmiots/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FeatureId,LengthBeforeOperation,WidthBeforeOperation,HeightBeforeOperation,LengthAfterOperation,WidthAfterOperation,HeightAfterOperation,HasPreviousFeature,DrillDiameter,DrillDepth,DrillApplicationCount,ReamingDiameter,ReamingDepth,FaceMillingDepth,FinishingMillingDepth,PocketLength,PocketWidth,PocketDepth,AddFinishingOperation,SlotHeight,SlotPlane,SlotApplicationCount,StepHeight,StepWidth,StepPlane,VolumeToRemove,VolumeToRemoveFinish,AccuracyClassId,SurfaceRoughnessId,FinishingAccuracyClassId,FinishingSurfaceRoughnessId")] Przedmiot przedmiot)
        {
            int? selectedProjectId = HttpContext.Session.GetInt32("SelectedProjectId");
            if (selectedProjectId == null)
            {
                return RedirectToAction("Index", "Projekts");
            }

            if (ModelState.IsValid)
            {
                // Pobierz projekt wraz z półfabrykatem
                var projekt = await _context.Projekts
                    .Include(p => p.SemiFinishedProduct)
                    .FirstOrDefaultAsync(p => p.Id == selectedProjectId);

                if (projekt == null || projekt.SemiFinishedProduct == null)
                {
                    return NotFound("Projekt or SemiFinishedProduct not found.");
                }

                var semiFinishedProduct = projekt.SemiFinishedProduct;

                // Pobierz wymiary z poprzedniej cechy (Feature), jeśli istnieje
                var previousFeature = await _context.Przedmiots
                    .Where(p => p.ProjektId == selectedProjectId && p.HasPreviousFeature)
                    .OrderByDescending(p => p.Id)
                    .FirstOrDefaultAsync();

                if (previousFeature != null)
                {
                    przedmiot.LengthBeforeOperation = previousFeature.LengthAfterOperation;
                    przedmiot.WidthBeforeOperation = previousFeature.WidthAfterOperation;
                    przedmiot.HeightBeforeOperation = previousFeature.HeightAfterOperation;
                }
                else
                {
                    przedmiot.LengthBeforeOperation = semiFinishedProduct.DimensionX;
                    przedmiot.WidthBeforeOperation = semiFinishedProduct.DimensionY;
                    przedmiot.HeightBeforeOperation = semiFinishedProduct.DimensionZ;
                }

                // Aktualizacja wymiarów na podstawie typu operacji (Feature)
                var feature = await _context.Features.FindAsync(przedmiot.FeatureId);
                if (feature == null)
                {
                    return NotFound("Feature not found.");
                }

                switch (feature.Name)
                {
                    case "Płaszczyzna Górna":
                        // Frezowanie powierzchni czołowej 
                        przedmiot.VolumeToRemove = przedmiot.FaceMillingDepth.GetValueOrDefault() * przedmiot.LengthBeforeOperation * przedmiot.WidthBeforeOperation;
                        przedmiot.VolumeToRemoveFinish = przedmiot.FinishingMillingDepth.GetValueOrDefault() * przedmiot.LengthBeforeOperation * przedmiot.WidthBeforeOperation;
                        przedmiot.LengthAfterOperation = przedmiot.LengthBeforeOperation;
                        przedmiot.WidthAfterOperation = przedmiot.WidthBeforeOperation;
                        przedmiot.HeightAfterOperation = przedmiot.HeightBeforeOperation - przedmiot.FaceMillingDepth.GetValueOrDefault() - przedmiot.FinishingMillingDepth.GetValueOrDefault();
                        break;

                    case "Otwór":
                        var radius = przedmiot.DrillDiameter.GetValueOrDefault() / 2;
                        przedmiot.VolumeToRemove = przedmiot.DrillApplicationCount.GetValueOrDefault() * (decimal)Math.PI * radius * radius * przedmiot.DrillDepth.GetValueOrDefault();
                        przedmiot.LengthAfterOperation = przedmiot.LengthBeforeOperation;
                        przedmiot.WidthAfterOperation = przedmiot.WidthBeforeOperation;
                        przedmiot.HeightAfterOperation = przedmiot.HeightBeforeOperation;

                        // Dodanie rozwiercania
                        if (przedmiot.ReamingDiameter.HasValue && przedmiot.ReamingDepth.HasValue)
                        {
                            var reamingRadius = przedmiot.ReamingDiameter.GetValueOrDefault() / 2;
                            przedmiot.VolumeToRemoveFinish += przedmiot.DrillApplicationCount.GetValueOrDefault() * (decimal)Math.PI * reamingRadius * reamingRadius * przedmiot.ReamingDepth.GetValueOrDefault();
                        }
                        break;

                    case "Kieszeń Zamknięta":
                        var pocketVolume = przedmiot.PocketDepth.GetValueOrDefault() * przedmiot.PocketLength.GetValueOrDefault() * przedmiot.PocketWidth.GetValueOrDefault();
                        przedmiot.VolumeToRemove = pocketVolume;

                        if (przedmiot.AddFinishingOperation.HasValue)
                        {
                            var finishingPocketVolume = przedmiot.AddFinishingOperation.GetValueOrDefault() * przedmiot.PocketLength.GetValueOrDefault() * przedmiot.PocketWidth.GetValueOrDefault();
                            przedmiot.VolumeToRemoveFinish = finishingPocketVolume;
                        }

                        przedmiot.LengthAfterOperation = przedmiot.LengthBeforeOperation;
                        przedmiot.WidthAfterOperation = przedmiot.WidthBeforeOperation;
                        przedmiot.HeightAfterOperation = przedmiot.HeightBeforeOperation;
                        break;

                    case "Rowek Przelotowy":
                        if (przedmiot.SlotPlane == true)
                        {
                            przedmiot.VolumeToRemove = przedmiot.SlotHeight.GetValueOrDefault() * przedmiot.SlotApplicationCount.GetValueOrDefault() * przedmiot.WidthBeforeOperation;
                            przedmiot.LengthAfterOperation = przedmiot.LengthBeforeOperation - przedmiot.SlotHeight.GetValueOrDefault();
                        }
                        else
                        {
                            przedmiot.VolumeToRemove = przedmiot.SlotHeight.GetValueOrDefault() * przedmiot.SlotApplicationCount.GetValueOrDefault() * przedmiot.LengthBeforeOperation;
                            przedmiot.WidthAfterOperation = przedmiot.WidthBeforeOperation - przedmiot.SlotHeight.GetValueOrDefault();
                        }
                        przedmiot.HeightAfterOperation = przedmiot.HeightBeforeOperation;
                        break;

                    case "Uskok":
                        if (przedmiot.StepPlane == true)
                        {
                            przedmiot.VolumeToRemove = przedmiot.StepHeight.GetValueOrDefault() * przedmiot.StepWidth.GetValueOrDefault() * przedmiot.WidthBeforeOperation;
                            przedmiot.LengthAfterOperation = przedmiot.LengthBeforeOperation - przedmiot.StepHeight.GetValueOrDefault();
                        }
                        else
                        {
                            przedmiot.VolumeToRemove = przedmiot.StepHeight.GetValueOrDefault() * przedmiot.StepWidth.GetValueOrDefault() * przedmiot.LengthBeforeOperation;
                            przedmiot.WidthAfterOperation = przedmiot.WidthBeforeOperation - przedmiot.StepWidth.GetValueOrDefault();
                        }
                        przedmiot.HeightAfterOperation = przedmiot.HeightBeforeOperation;
                        break;
                }

                Console.WriteLine($"Length Before: {przedmiot.LengthBeforeOperation}");
                Console.WriteLine($"Length After: {przedmiot.LengthAfterOperation}");
                Console.WriteLine($"Width Before: {przedmiot.WidthBeforeOperation}");
                Console.WriteLine($"Width After: {przedmiot.WidthAfterOperation}");
                Console.WriteLine($"Height Before: {przedmiot.HeightBeforeOperation}");
                Console.WriteLine($"Height After: {przedmiot.HeightAfterOperation}");
                Console.WriteLine($"Volume to Remove: {przedmiot.VolumeToRemove}");
                Console.WriteLine($"Volume to Remove Finish: {przedmiot.VolumeToRemoveFinish}");

                przedmiot.ProjektId = selectedProjectId.Value;
                _context.Add(przedmiot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["FeatureId"] = new SelectList(_context.Features, "Id", "Name", przedmiot.FeatureId);
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "Id", "Id", przedmiot.ProjektId);
            ViewData["AccuracyClassId"] = new SelectList(_context.AccuracyClasses, "Id", "Name", przedmiot.AccuracyClassId);
            ViewData["SurfaceRoughnessId"] = new SelectList(_context.SurfaceRoughnesses, "Id", "Name", przedmiot.SurfaceRoughnessId);
            ViewData["FinishingAccuracyClassId"] = new SelectList(_context.FinishingAccuracyClasses, "Id", "Name", przedmiot.FinishingAccuracyClassId);
            ViewData["FinishingSurfaceRoughnessId"] = new SelectList(_context.FinishingSurfaceRoughnesses, "Id", "Name", przedmiot.FinishingSurfaceRoughnessId);
            return View(przedmiot);
        }

        // GET: Przedmiots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Przedmiots == null)
            {
                return NotFound();
            }

            var przedmiot = await _context.Przedmiots.FindAsync(id);
            if (przedmiot == null)
            {
                return NotFound();
            }
            ViewData["FeatureId"] = new SelectList(_context.Features, "Id", "Name", przedmiot.FeatureId);
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "Id", "Id", przedmiot.ProjektId);
            ViewData["AccuracyClassId"] = new SelectList(_context.AccuracyClasses, "Id", "Name", przedmiot.AccuracyClassId);
            ViewData["SurfaceRoughnessId"] = new SelectList(_context.SurfaceRoughnesses, "Id", "Name", przedmiot.SurfaceRoughnessId);
            ViewData["FinishingAccuracyClassId"] = new SelectList(_context.FinishingAccuracyClasses, "Id", "Name", przedmiot.FinishingAccuracyClassId);
            ViewData["FinishingSurfaceRoughnessId"] = new SelectList(_context.FinishingSurfaceRoughnesses, "Id", "Name", przedmiot.FinishingSurfaceRoughnessId);
            return View(przedmiot);
        }

        // POST: Przedmiots/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FeatureId,LengthBeforeOperation,WidthBeforeOperation,HeightBeforeOperation,LengthAfterOperation,WidthAfterOperation,HeightAfterOperation,HasPreviousFeature,DrillDiameter,DrillDepth,DrillApplicationCount,ReamingDiameter,ReamingDepth,FaceMillingDepth,FinishingMillingDepth,PocketLength,PocketWidth,PocketDepth,AddFinishingOperation,SlotHeight,SlotPlane,SlotApplicationCount,StepHeight,StepWidth,StepPlane,VolumeToRemove,VolumeToRemoveFinish,AccuracyClassId,SurfaceRoughnessId,FinishingAccuracyClassId,FinishingSurfaceRoughnessId")] Przedmiot przedmiot)
        {
            if (id != przedmiot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(przedmiot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrzedmiotExists(przedmiot.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FeatureId"] = new SelectList(_context.Features, "Id", "Name", przedmiot.FeatureId);
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "Id", "Id", przedmiot.ProjektId);
            ViewData["AccuracyClassId"] = new SelectList(_context.AccuracyClasses, "Id", "Name", przedmiot.AccuracyClassId);
            ViewData["SurfaceRoughnessId"] = new SelectList(_context.SurfaceRoughnesses, "Id", "Name", przedmiot.SurfaceRoughnessId);
            ViewData["FinishingAccuracyClassId"] = new SelectList(_context.FinishingAccuracyClasses, "Id", "Name", przedmiot.FinishingAccuracyClassId);
            ViewData["FinishingSurfaceRoughnessId"] = new SelectList(_context.FinishingSurfaceRoughnesses, "Id", "Name", przedmiot.FinishingSurfaceRoughnessId);
            return View(przedmiot);
        }

        // GET: Przedmiots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Przedmiots == null)
            {
                return NotFound();
            }

            var przedmiot = await _context.Przedmiots
                .Include(p => p.Feature)
                .Include(p => p.Projekt)
                .Include(p => p.AccuracyClass)
                .Include(p => p.SurfaceRoughness)
                .Include(p => p.FinishingAccuracyClass)
                .Include(p => p.FinishingSurfaceRoughness)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (przedmiot == null)
            {
                return NotFound();
            }

            return View(przedmiot);
        }

        // POST: Przedmiots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Przedmiots == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Przedmiots' is null.");
            }
            var przedmiot = await _context.Przedmiots.FindAsync(id);
            if (przedmiot != null)
            {
                _context.Przedmiots.Remove(przedmiot);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrzedmiotExists(int id)
        {
            return (_context.Przedmiots?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult GoToOperationSets()
        {
            int? selectedProjectId = HttpContext.Session.GetInt32("SelectedProjectId");
            if (selectedProjectId == null)
            {
                return RedirectToAction("Index", "Projekts");
            }

            return RedirectToAction("Index", "OperationSets");
        }
    }
}
