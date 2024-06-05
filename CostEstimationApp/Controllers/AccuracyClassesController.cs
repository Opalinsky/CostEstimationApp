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
    public class AccuracyClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccuracyClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AccuracyClasses
        public async Task<IActionResult> Index()
        {
              return _context.AccuracyClasses != null ? 
                          View(await _context.AccuracyClasses.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AccuracyClasses'  is null.");
        }

        // GET: AccuracyClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AccuracyClasses == null)
            {
                return NotFound();
            }

            var accuracyClass = await _context.AccuracyClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accuracyClass == null)
            {
                return NotFound();
            }

            return View(accuracyClass);
        }

        // GET: AccuracyClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccuracyClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] AccuracyClass accuracyClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accuracyClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accuracyClass);
        }

        // GET: AccuracyClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AccuracyClasses == null)
            {
                return NotFound();
            }

            var accuracyClass = await _context.AccuracyClasses.FindAsync(id);
            if (accuracyClass == null)
            {
                return NotFound();
            }
            return View(accuracyClass);
        }

        // POST: AccuracyClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] AccuracyClass accuracyClass)
        {
            if (id != accuracyClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accuracyClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccuracyClassExists(accuracyClass.Id))
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
            return View(accuracyClass);
        }

        // GET: AccuracyClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AccuracyClasses == null)
            {
                return NotFound();
            }

            var accuracyClass = await _context.AccuracyClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accuracyClass == null)
            {
                return NotFound();
            }

            return View(accuracyClass);
        }

        // POST: AccuracyClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AccuracyClasses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AccuracyClasses'  is null.");
            }
            var accuracyClass = await _context.AccuracyClasses.FindAsync(id);
            if (accuracyClass != null)
            {
                _context.AccuracyClasses.Remove(accuracyClass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccuracyClassExists(int id)
        {
          return (_context.AccuracyClasses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
