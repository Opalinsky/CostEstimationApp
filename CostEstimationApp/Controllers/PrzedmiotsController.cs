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
            return View();
        }

        // POST: Przedmiots/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FeatureId,DrillDiameter,DrillDepth,DrillApplicationCount,FaceMillingDepth,FinishingMillingDepth,AddFinishingMilling,PocketLength,PocketWidth,PocketDepth,AddFinishingOperation,SlotHeight,WhichSurface,SlotApplicationCount")] Przedmiot przedmiot)
        {
            int? selectedProjectId = HttpContext.Session.GetInt32("SelectedProjectId");
            if (selectedProjectId == null)
            {
                return RedirectToAction("Index", "Projekts");
            }

            if (ModelState.IsValid)
            {
                przedmiot.ProjektId = selectedProjectId.Value;
                _context.Add(przedmiot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["FeatureId"] = new SelectList(_context.Features, "Id", "Name", przedmiot.FeatureId);
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "Id", "Id", przedmiot.ProjektId);
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
            return View(przedmiot);
        }

        // POST: Przedmiots/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ProjektId,FeatureId,DrillDiameter,DrillDepth,DrillApplicationCount,FaceMillingDepth,FinishingMillingDepth,AddFinishingMilling,PocketLength,PocketWidth,PocketDepth,AddFinishingOperation,SlotHeight,WhichSurface,SlotApplicationCount")] Przedmiot przedmiot)
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
                return Problem("Entity set 'ApplicationDbContext.Przedmiots'  is null.");
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
