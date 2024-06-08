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
            return View();
        }

        // POST: Przedmiots/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FeatureId,LengthBeforeOperation,WidthBeforeOperation,HeightBeforeOperation,LengthAfterOperation,WidthAfterOperation,HeightAfterOperation,HasPreviousFeature,DrillDiameter,DrillDepth,DrillApplicationCount,ReamingDiameter,ReamingDepth,FaceMillingDepth,FinishingMillingDepth,PocketLength,PocketWidth,PocketDepth,AddFinishingOperation,SlotHeight,SlotPlane,SlotHeightFinish,SlotApplicationCount,StepHeight,StepWidth,StepPlane,VolumeToRemove,VolumeToRemoveFinish,AccuracyClassId,DrillingDepthFinish,SlotHeightFinish,StepHeightFinish")] Przedmiot przedmiot)
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
                var accuracyClass = await _context.AccuracyClasses.FindAsync(przedmiot.AccuracyClassId);


                switch (feature.Name)
                {
                    case "Płaszczyzna Górna":
                        if (przedmiot.FaceMillingDepth > 0.7m)
                        {
                            if (accuracyClass.Name == "IT11" || accuracyClass.Name == "IT12" || accuracyClass.Name == "IT13")
                            {
                                przedmiot.VolumeToRemove = przedmiot.FaceMillingDepth.GetValueOrDefault() * przedmiot.LengthBeforeOperation * przedmiot.WidthBeforeOperation;
                                przedmiot.LengthAfterOperation = przedmiot.LengthBeforeOperation;
                            }
                            else if (accuracyClass.Name == "IT5" || accuracyClass.Name == "IT6" || accuracyClass.Name == "IT7")
                            {
                                przedmiot.FinishingMillingDepth = 0.5m;
                                przedmiot.FaceMillingDepth -= 0.5m;
                                przedmiot.VolumeToRemove = przedmiot.FaceMillingDepth.GetValueOrDefault() * przedmiot.LengthBeforeOperation * przedmiot.WidthBeforeOperation;
                                przedmiot.VolumeToRemoveFinish = przedmiot.FinishingMillingDepth.GetValueOrDefault() * przedmiot.LengthBeforeOperation * przedmiot.WidthBeforeOperation;
                            }
                        }
                        else
                        {
                            przedmiot.FinishingMillingDepth = przedmiot.FaceMillingDepth;
                            przedmiot.FaceMillingDepth = 0;
                            przedmiot.VolumeToRemoveFinish = przedmiot.FinishingMillingDepth.GetValueOrDefault() * przedmiot.LengthBeforeOperation * przedmiot.WidthBeforeOperation;
                        }
                        przedmiot.LengthAfterOperation = przedmiot.LengthBeforeOperation;
                        przedmiot.WidthAfterOperation = przedmiot.WidthBeforeOperation;
                        przedmiot.HeightAfterOperation = przedmiot.HeightBeforeOperation - przedmiot.FaceMillingDepth.GetValueOrDefault() - przedmiot.FinishingMillingDepth.GetValueOrDefault();

                        break;

                    case "Kieszeń Zamknięta":
                        if (przedmiot.PocketDepth > 0.7m)
                        {
                            if (accuracyClass.Name == "IT11" || accuracyClass.Name == "IT12" || accuracyClass.Name == "IT13")
                            {
                                przedmiot.VolumeToRemove = przedmiot.PocketDepth.GetValueOrDefault() * przedmiot.PocketLength.GetValueOrDefault() * przedmiot.PocketWidth.GetValueOrDefault();
                            }
                            else if (accuracyClass.Name == "IT5" || accuracyClass.Name == "IT6" || accuracyClass.Name == "IT7")
                            {
                                przedmiot.AddFinishingOperation = 0.5m;
                                przedmiot.PocketDepth -= 0.5m;
                                przedmiot.VolumeToRemove = przedmiot.PocketDepth.GetValueOrDefault() * przedmiot.PocketLength.GetValueOrDefault() * przedmiot.PocketWidth.GetValueOrDefault();
                                przedmiot.VolumeToRemoveFinish = przedmiot.AddFinishingOperation.GetValueOrDefault() * przedmiot.PocketLength.GetValueOrDefault() * przedmiot.PocketWidth.GetValueOrDefault();

                            }
                        }
                        else
                        {
                            przedmiot.AddFinishingOperation = przedmiot.PocketDepth;
                            przedmiot.PocketDepth = 0;
                            przedmiot.VolumeToRemoveFinish = przedmiot.AddFinishingOperation.GetValueOrDefault() * przedmiot.PocketLength.GetValueOrDefault() * przedmiot.PocketWidth.GetValueOrDefault();
                        }
                        przedmiot.LengthAfterOperation = przedmiot.LengthBeforeOperation;
                        przedmiot.WidthAfterOperation = przedmiot.WidthBeforeOperation;
                        przedmiot.HeightAfterOperation = przedmiot.HeightBeforeOperation - przedmiot.AddFinishingOperation.GetValueOrDefault() - przedmiot.PocketDepth.GetValueOrDefault();
                        break;

                    case "Otwór":
                        if (przedmiot.DrillDepth > 0.7m && !przedmiot.ReamingDiameter.HasValue && !przedmiot.ReamingDepth.HasValue)
                        {
                            var radius = przedmiot.DrillDiameter.GetValueOrDefault() / 2;
                            if (accuracyClass.Name == "IT11" || accuracyClass.Name == "IT12" || accuracyClass.Name == "IT13")
                            {
                                przedmiot.VolumeToRemove = przedmiot.DrillApplicationCount.GetValueOrDefault() * (decimal)Math.PI * radius * radius * przedmiot.DrillDepth.GetValueOrDefault();
                            }
                            else if (accuracyClass.Name == "IT5" || accuracyClass.Name == "IT6" || accuracyClass.Name == "IT7")
                            {
                                przedmiot.DrillingDepthFinish = 0.5m;
                                przedmiot.DrillDepth -= 0.5m;
                                przedmiot.VolumeToRemove = przedmiot.DrillApplicationCount.GetValueOrDefault() * (decimal)Math.PI * radius * radius * przedmiot.DrillDepth.GetValueOrDefault();
                                przedmiot.VolumeToRemoveFinish = przedmiot.DrillApplicationCount.GetValueOrDefault() * (decimal)Math.PI * radius * radius * przedmiot.DrillingDepthFinish.GetValueOrDefault();
                            }
                        }
                        else if (przedmiot.DrillDepth > 0.7m && przedmiot.ReamingDiameter.HasValue && przedmiot.ReamingDepth.HasValue)
                        {
                            var radius = przedmiot.DrillDiameter.GetValueOrDefault() / 2;
                            var radiusRim = przedmiot.ReamingDiameter.GetValueOrDefault() / 2;
                            if (accuracyClass.Name == "IT11" || accuracyClass.Name == "IT12" || accuracyClass.Name == "IT13")
                            {
                                przedmiot.VolumeToRemove = przedmiot.DrillApplicationCount.GetValueOrDefault() * (decimal)Math.PI * radius * radius * przedmiot.DrillDepth.GetValueOrDefault();
                                przedmiot.VolumeToRemoveFinish = przedmiot.DrillApplicationCount.GetValueOrDefault() * ((decimal)Math.PI * radiusRim * radiusRim * przedmiot.ReamingDepth.GetValueOrDefault()) - (decimal)Math.PI * radius * radius * przedmiot.DrillingDepthFinish.GetValueOrDefault();
                            }
                            else if (accuracyClass.Name == "IT5" || accuracyClass.Name == "IT6" || accuracyClass.Name == "IT7")
                            {
                                przedmiot.VolumeToRemove = przedmiot.DrillApplicationCount.GetValueOrDefault() * (decimal)Math.PI * radius * radius * przedmiot.DrillDepth.GetValueOrDefault();
                                przedmiot.VolumeToRemoveFinish = (przedmiot.DrillApplicationCount.GetValueOrDefault() * ((decimal)Math.PI * radiusRim * radiusRim * przedmiot.ReamingDepth.GetValueOrDefault()) - ((decimal)Math.PI * radius * radius * przedmiot.DrillingDepthFinish.GetValueOrDefault()));
                            }
                        }
                        else
                        {
                            if (przedmiot.ReamingDiameter.HasValue && przedmiot.ReamingDepth.HasValue)
                            {
                                var radiusRim = przedmiot.ReamingDiameter.GetValueOrDefault() / 2;
                                przedmiot.VolumeToRemoveFinish = przedmiot.DrillApplicationCount.GetValueOrDefault() * (decimal)Math.PI * radiusRim * radiusRim * przedmiot.ReamingDepth.GetValueOrDefault();
                                przedmiot.DrillDepth = 0;
                                przedmiot.DrillDiameter = 0;
                            }
                            else
                            {
                                var radius = przedmiot.DrillDiameter.GetValueOrDefault() / 2;
                                przedmiot.DrillingDepthFinish = przedmiot.DrillDepth;
                                przedmiot.DrillDepth = 0;
                                przedmiot.VolumeToRemoveFinish = przedmiot.DrillApplicationCount.GetValueOrDefault() * (decimal)Math.PI * radius * radius * przedmiot.DrillingDepthFinish.GetValueOrDefault();
                            }
                        }

                        przedmiot.LengthAfterOperation = przedmiot.LengthBeforeOperation;
                        przedmiot.WidthAfterOperation = przedmiot.WidthBeforeOperation;
                        przedmiot.HeightAfterOperation = przedmiot.HeightBeforeOperation;
                        break;

                    case "Rowek Przelotowy":
                        if (przedmiot.SlotHeight > 0.7m)
                        {
                            if (przedmiot.SlotPlane == true)
                            {
                                if (accuracyClass.Name == "IT11" || accuracyClass.Name == "IT12" || accuracyClass.Name == "IT13")
                                {
                                    przedmiot.VolumeToRemove = przedmiot.SlotHeight.GetValueOrDefault() * przedmiot.LengthBeforeOperation;
                                    Console.WriteLine("1");
                                }
                                else if (accuracyClass.Name == "IT5" || accuracyClass.Name == "IT6" || accuracyClass.Name == "IT7")
                                {
                                    przedmiot.SlotHeightFinish = 0.5m;
                                    przedmiot.SlotHeight -= 0.5m;
                                    przedmiot.VolumeToRemove = przedmiot.SlotHeight.GetValueOrDefault() * przedmiot.LengthBeforeOperation;
                                    przedmiot.VolumeToRemoveFinish = przedmiot.SlotHeightFinish.GetValueOrDefault() * przedmiot.LengthBeforeOperation;
                                    Console.WriteLine("2");
                                }
                            }
                            else
                            {
                                if (accuracyClass.Name == "IT11" || accuracyClass.Name == "IT12" || accuracyClass.Name == "IT13")
                                {
                                    przedmiot.VolumeToRemove = przedmiot.SlotHeight.GetValueOrDefault() * przedmiot.WidthBeforeOperation;
                                    Console.WriteLine("3");
                                }
                                else if (accuracyClass.Name == "IT5" || accuracyClass.Name == "IT6" || accuracyClass.Name == "IT7")
                                {
                                    przedmiot.SlotHeightFinish = 0.5m;
                                    przedmiot.SlotHeight -= 0.5m;
                                    przedmiot.VolumeToRemove = przedmiot.SlotHeight.GetValueOrDefault() * przedmiot.LengthBeforeOperation;
                                    przedmiot.VolumeToRemoveFinish = przedmiot.SlotHeightFinish.GetValueOrDefault() * przedmiot.WidthBeforeOperation;
                                    Console.WriteLine("4");
                                }
                            }
                        }
                        else
                        {
                            if (przedmiot.SlotPlane == true)
                            {
                                    przedmiot.SlotHeightFinish = przedmiot.SlotHeight;
                                    przedmiot.SlotHeight = 0;
                                    przedmiot.VolumeToRemoveFinish = przedmiot.SlotHeightFinish.GetValueOrDefault() * przedmiot.LengthBeforeOperation;
                                Console.WriteLine("5");
                            }
                            else
                            {
                                przedmiot.SlotHeightFinish = przedmiot.SlotHeight;
                                przedmiot.SlotHeight = 0;
                                przedmiot.VolumeToRemoveFinish = przedmiot.SlotHeightFinish.GetValueOrDefault() * przedmiot.WidthBeforeOperation;
                                Console.WriteLine("6");
                            }
                        }
                        przedmiot.LengthAfterOperation = przedmiot.LengthBeforeOperation;
                        przedmiot.WidthAfterOperation = przedmiot.WidthBeforeOperation;
                        przedmiot.HeightAfterOperation = przedmiot.HeightBeforeOperation;
                        break;

                    case "Uskok":
                        if (przedmiot.StepHeight > 0.7m)
                        {
                            if (przedmiot.StepPlane == true)
                            {
                                if (accuracyClass.Name == "IT11" || accuracyClass.Name == "IT12" || accuracyClass.Name == "IT13")
                                {
                                    przedmiot.VolumeToRemove = przedmiot.StepHeight.GetValueOrDefault() * przedmiot.StepWidth.GetValueOrDefault() * przedmiot.LengthBeforeOperation;
                                    Console.WriteLine("1");
                                }
                                else if (accuracyClass.Name == "IT5" || accuracyClass.Name == "IT6" || accuracyClass.Name == "IT7")
                                {
                                    przedmiot.StepHeightFinish = 0.5m;
                                    przedmiot.StepHeight -= 0.5m;
                                    przedmiot.VolumeToRemove = przedmiot.StepHeight.GetValueOrDefault() * przedmiot.StepWidth.GetValueOrDefault() * przedmiot.LengthBeforeOperation;
                                    przedmiot.VolumeToRemoveFinish = przedmiot.StepHeightFinish.GetValueOrDefault() * przedmiot.StepWidth.GetValueOrDefault() * przedmiot.LengthBeforeOperation;
                                    Console.WriteLine("2");
                                }
                            }
                            else
                            {
                                if (accuracyClass.Name == "IT11" || accuracyClass.Name == "IT12" || accuracyClass.Name == "IT13")
                                {
                                    przedmiot.VolumeToRemove = przedmiot.StepHeight.GetValueOrDefault() * przedmiot.StepWidth.GetValueOrDefault() * przedmiot.WidthBeforeOperation;
                                    Console.WriteLine("1");
                                }
                                else if (accuracyClass.Name == "IT5" || accuracyClass.Name == "IT6" || accuracyClass.Name == "IT7")
                                {
                                    przedmiot.StepHeightFinish = 0.5m;
                                    przedmiot.StepHeight -= 0.5m;
                                    przedmiot.VolumeToRemove = przedmiot.StepHeight.GetValueOrDefault() * przedmiot.StepWidth.GetValueOrDefault() * przedmiot.LengthBeforeOperation;
                                    przedmiot.VolumeToRemoveFinish = przedmiot.StepHeightFinish.GetValueOrDefault() * przedmiot.StepWidth.GetValueOrDefault() * przedmiot.WidthBeforeOperation;
                                    Console.WriteLine("2");
                                }
                            }
                        }
                        else
                        {
                            if (przedmiot.StepPlane == true)
                            {
                                przedmiot.StepHeightFinish = przedmiot.StepHeight;
                                przedmiot.StepHeight = 0;
                                przedmiot.VolumeToRemoveFinish = przedmiot.StepHeightFinish.GetValueOrDefault() * przedmiot.StepWidth.GetValueOrDefault() * przedmiot.LengthBeforeOperation;
                                Console.WriteLine("5");
                            }
                            else
                            {
                                przedmiot.StepHeightFinish = przedmiot.StepHeight;
                                przedmiot.StepHeight = 0;
                                przedmiot.VolumeToRemoveFinish = przedmiot.StepHeightFinish.GetValueOrDefault() * przedmiot.StepWidth.GetValueOrDefault() * przedmiot.WidthBeforeOperation;
                                Console.WriteLine("5");
                            }
                        }
                        przedmiot.LengthAfterOperation = przedmiot.LengthBeforeOperation;
                        przedmiot.WidthAfterOperation = przedmiot.WidthBeforeOperation;
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
            return View(przedmiot);
        }

        // POST: Przedmiots/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FeatureId,LengthBeforeOperation,WidthBeforeOperation,HeightBeforeOperation,LengthAfterOperation,WidthAfterOperation,HeightAfterOperation,HasPreviousFeature,DrillDiameter,DrillDepth,DrillApplicationCount,ReamingDiameter,ReamingDepth,FaceMillingDepth,FinishingMillingDepth,PocketLength,PocketWidth,PocketDepth,AddFinishingOperation,SlotHeight,SlotPlane,SlotHeightFinish,SlotApplicationCount,StepHeight,StepWidth,StepPlane,VolumeToRemove,VolumeToRemoveFinish,AccuracyClassId,DrillingDepthFinish,SlotHeightFinish,StepHeightFinish")] Przedmiot przedmiot)
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
