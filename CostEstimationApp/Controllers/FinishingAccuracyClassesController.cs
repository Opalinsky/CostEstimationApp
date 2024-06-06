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
    public class FinishingAccuracyClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FinishingAccuracyClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FinishingAccuracyClasses
        public async Task<IActionResult> Index()
        {
              return _context.FinishingAccuracyClass != null ? 
                          View(await _context.FinishingAccuracyClass.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.FinishingAccuracyClass'  is null.");
        }

        // GET: FinishingAccuracyClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FinishingAccuracyClass == null)
            {
                return NotFound();
            }

            var finishingAccuracyClass = await _context.FinishingAccuracyClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (finishingAccuracyClass == null)
            {
                return NotFound();
            }

            return View(finishingAccuracyClass);
        }

        // GET: FinishingAccuracyClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FinishingAccuracyClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] FinishingAccuracyClass finishingAccuracyClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(finishingAccuracyClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(finishingAccuracyClass);
        }

        // GET: FinishingAccuracyClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FinishingAccuracyClass == null)
            {
                return NotFound();
            }

            var finishingAccuracyClass = await _context.FinishingAccuracyClass.FindAsync(id);
            if (finishingAccuracyClass == null)
            {
                return NotFound();
            }
            return View(finishingAccuracyClass);
        }

        // POST: FinishingAccuracyClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] FinishingAccuracyClass finishingAccuracyClass)
        {
            if (id != finishingAccuracyClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(finishingAccuracyClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinishingAccuracyClassExists(finishingAccuracyClass.Id))
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
            return View(finishingAccuracyClass);
        }

        // GET: FinishingAccuracyClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FinishingAccuracyClass == null)
            {
                return NotFound();
            }

            var finishingAccuracyClass = await _context.FinishingAccuracyClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (finishingAccuracyClass == null)
            {
                return NotFound();
            }

            return View(finishingAccuracyClass);
        }

        // POST: FinishingAccuracyClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FinishingAccuracyClass == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FinishingAccuracyClass'  is null.");
            }
            var finishingAccuracyClass = await _context.FinishingAccuracyClass.FindAsync(id);
            if (finishingAccuracyClass != null)
            {
                _context.FinishingAccuracyClass.Remove(finishingAccuracyClass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinishingAccuracyClassExists(int id)
        {
          return (_context.FinishingAccuracyClass?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
