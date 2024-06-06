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
    public class FinishingSurfaceRoughnessesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FinishingSurfaceRoughnessesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FinishingSurfaceRoughnesses
        public async Task<IActionResult> Index()
        {
              return _context.FinishingSurfaceRoughness != null ? 
                          View(await _context.FinishingSurfaceRoughness.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.FinishingSurfaceRoughness'  is null.");
        }

        // GET: FinishingSurfaceRoughnesses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FinishingSurfaceRoughness == null)
            {
                return NotFound();
            }

            var finishingSurfaceRoughness = await _context.FinishingSurfaceRoughness
                .FirstOrDefaultAsync(m => m.Id == id);
            if (finishingSurfaceRoughness == null)
            {
                return NotFound();
            }

            return View(finishingSurfaceRoughness);
        }

        // GET: FinishingSurfaceRoughnesses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FinishingSurfaceRoughnesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] FinishingSurfaceRoughness finishingSurfaceRoughness)
        {
            if (ModelState.IsValid)
            {
                _context.Add(finishingSurfaceRoughness);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(finishingSurfaceRoughness);
        }

        // GET: FinishingSurfaceRoughnesses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FinishingSurfaceRoughness == null)
            {
                return NotFound();
            }

            var finishingSurfaceRoughness = await _context.FinishingSurfaceRoughness.FindAsync(id);
            if (finishingSurfaceRoughness == null)
            {
                return NotFound();
            }
            return View(finishingSurfaceRoughness);
        }

        // POST: FinishingSurfaceRoughnesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] FinishingSurfaceRoughness finishingSurfaceRoughness)
        {
            if (id != finishingSurfaceRoughness.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(finishingSurfaceRoughness);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinishingSurfaceRoughnessExists(finishingSurfaceRoughness.Id))
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
            return View(finishingSurfaceRoughness);
        }

        // GET: FinishingSurfaceRoughnesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FinishingSurfaceRoughness == null)
            {
                return NotFound();
            }

            var finishingSurfaceRoughness = await _context.FinishingSurfaceRoughness
                .FirstOrDefaultAsync(m => m.Id == id);
            if (finishingSurfaceRoughness == null)
            {
                return NotFound();
            }

            return View(finishingSurfaceRoughness);
        }

        // POST: FinishingSurfaceRoughnesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FinishingSurfaceRoughness == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FinishingSurfaceRoughness'  is null.");
            }
            var finishingSurfaceRoughness = await _context.FinishingSurfaceRoughness.FindAsync(id);
            if (finishingSurfaceRoughness != null)
            {
                _context.FinishingSurfaceRoughness.Remove(finishingSurfaceRoughness);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinishingSurfaceRoughnessExists(int id)
        {
          return (_context.FinishingSurfaceRoughness?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
