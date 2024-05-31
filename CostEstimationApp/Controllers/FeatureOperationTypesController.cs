using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CostEstimationApp.Data;
using CostEstimationApp.Models;

namespace CostEstimationApp.Controllers
{
    public class FeatureOperationTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeatureOperationTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FeatureOperationTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FeatureOperationTypes.Include(f => f.Feature).Include(f => f.OperationType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FeatureOperationTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FeatureOperationTypes == null)
            {
                return NotFound();
            }

            var featureOperationType = await _context.FeatureOperationTypes
                .Include(f => f.Feature)
                .Include(f => f.OperationType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (featureOperationType == null)
            {
                return NotFound();
            }

            return View(featureOperationType);
        }

        // GET: FeatureOperationTypes/Create
        public IActionResult Create()
        {
            ViewData["FeatureId"] = new SelectList(_context.Features, "Id", "Name");
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name");
            return View();
        }

        // POST: FeatureOperationTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FeatureId,OperationTypeId")] FeatureOperationType featureOperationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(featureOperationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FeatureId"] = new SelectList(_context.Features, "Id", "Name", featureOperationType.FeatureId);
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name", featureOperationType.OperationTypeId);
            return View(featureOperationType);
        }

        // GET: FeatureOperationTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FeatureOperationTypes == null)
            {
                return NotFound();
            }

            var featureOperationType = await _context.FeatureOperationTypes.FindAsync(id);
            if (featureOperationType == null)
            {
                return NotFound();
            }
            ViewData["FeatureId"] = new SelectList(_context.Features, "Id", "Name", featureOperationType.FeatureId);
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name", featureOperationType.OperationTypeId);
            return View(featureOperationType);
        }

        // POST: FeatureOperationTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FeatureId,OperationTypeId")] FeatureOperationType featureOperationType)
        {
            if (id != featureOperationType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(featureOperationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeatureOperationTypeExists(featureOperationType.Id))
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
            ViewData["FeatureId"] = new SelectList(_context.Features, "Id", "Name", featureOperationType.FeatureId);
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name", featureOperationType.OperationTypeId);
            return View(featureOperationType);
        }

        // GET: FeatureOperationTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FeatureOperationTypes == null)
            {
                return NotFound();
            }

            var featureOperationType = await _context.FeatureOperationTypes
                .Include(f => f.Feature)
                .Include(f => f.OperationType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (featureOperationType == null)
            {
                return NotFound();
            }

            return View(featureOperationType);
        }

        // POST: FeatureOperationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FeatureOperationTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FeatureOperationTypes'  is null.");
            }
            var featureOperationType = await _context.FeatureOperationTypes.FindAsync(id);
            if (featureOperationType != null)
            {
                _context.FeatureOperationTypes.Remove(featureOperationType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeatureOperationTypeExists(int id)
        {
          return (_context.FeatureOperationTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
