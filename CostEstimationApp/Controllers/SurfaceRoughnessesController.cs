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
    public class SurfaceRoughnessesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SurfaceRoughnessesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SurfaceRoughnesses
        public async Task<IActionResult> Index()
        {
              return _context.SurfaceRoughnesses != null ? 
                          View(await _context.SurfaceRoughnesses.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.SurfaceRoughnesses'  is null.");
        }

        // GET: SurfaceRoughnesses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SurfaceRoughnesses == null)
            {
                return NotFound();
            }

            var surfaceRoughness = await _context.SurfaceRoughnesses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (surfaceRoughness == null)
            {
                return NotFound();
            }

            return View(surfaceRoughness);
        }

        // GET: SurfaceRoughnesses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SurfaceRoughnesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] SurfaceRoughness surfaceRoughness)
        {
            if (ModelState.IsValid)
            {
                _context.Add(surfaceRoughness);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(surfaceRoughness);
        }

        // GET: SurfaceRoughnesses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SurfaceRoughnesses == null)
            {
                return NotFound();
            }

            var surfaceRoughness = await _context.SurfaceRoughnesses.FindAsync(id);
            if (surfaceRoughness == null)
            {
                return NotFound();
            }
            return View(surfaceRoughness);
        }

        // POST: SurfaceRoughnesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] SurfaceRoughness surfaceRoughness)
        {
            if (id != surfaceRoughness.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(surfaceRoughness);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurfaceRoughnessExists(surfaceRoughness.Id))
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
            return View(surfaceRoughness);
        }

        // GET: SurfaceRoughnesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SurfaceRoughnesses == null)
            {
                return NotFound();
            }

            var surfaceRoughness = await _context.SurfaceRoughnesses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (surfaceRoughness == null)
            {
                return NotFound();
            }

            return View(surfaceRoughness);
        }

        // POST: SurfaceRoughnesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SurfaceRoughnesses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SurfaceRoughnesses'  is null.");
            }
            var surfaceRoughness = await _context.SurfaceRoughnesses.FindAsync(id);
            if (surfaceRoughness != null)
            {
                _context.SurfaceRoughnesses.Remove(surfaceRoughness);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurfaceRoughnessExists(int id)
        {
          return (_context.SurfaceRoughnesses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
