using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CostEstimationApp.Data;

namespace CostEstimationApp.Controllers
{
    public class ProcesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProcesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Proces
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Process.Include(p => p.Worker);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Proces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Process == null)
            {
                return NotFound();
            }

            var proces = await _context.Process
                .Include(p => p.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proces == null)
            {
                return NotFound();
            }

            return View(proces);
        }

        // GET: Proces/Create
        public IActionResult Create()
        {
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Id");
            return View();
        }

        // POST: Proces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PreparationTime,WorkerId,MachineCost,ToolCost,WorkerCost,TotalCost")] Proces proces)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proces);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Id", proces.WorkerId);
            return View(proces);
        }

        // GET: Proces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Process == null)
            {
                return NotFound();
            }

            var proces = await _context.Process.FindAsync(id);
            if (proces == null)
            {
                return NotFound();
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Id", proces.WorkerId);
            return View(proces);
        }

        // POST: Proces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PreparationTime,WorkerId,MachineCost,ToolCost,WorkerCost,TotalCost")] Proces proces)
        {
            if (id != proces.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proces);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcesExists(proces.Id))
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
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Id", proces.WorkerId);
            return View(proces);
        }

        // GET: Proces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Process == null)
            {
                return NotFound();
            }

            var proces = await _context.Process
                .Include(p => p.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proces == null)
            {
                return NotFound();
            }

            return View(proces);
        }

        // POST: Proces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Process == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Process'  is null.");
            }
            var proces = await _context.Process.FindAsync(id);
            if (proces != null)
            {
                _context.Process.Remove(proces);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcesExists(int id)
        {
          return (_context.Process?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
