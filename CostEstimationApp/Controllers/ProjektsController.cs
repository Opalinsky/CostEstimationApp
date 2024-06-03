using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CostEstimationApp.Data;
using CostEstimationApp.Models;

namespace CostEstimationApp.Controllers
{
    public class ProjektsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjektsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Projekts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Projekts.Include(p => p.SemiFinishedProduct);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Projekts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projekts == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekts
                .Include(p => p.SemiFinishedProduct)
                .Include(p => p.OperationSets)
                .ThenInclude(os => os.Operations)
                .ThenInclude(o => o.SemiFinishedProduct)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projekt == null)
            {
                return NotFound();
            }

            await CalculateTotalValue(projekt.Id);

            return View(projekt);
        }

        // GET: Projekts/Create
        public IActionResult Create()
        {
            ViewData["SemiFinishedProductId"] = new SelectList(_context.SemiFinishedProducts, "Id", "Id");
            return View();
        }

        // POST: Projekts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Quantity,SemiFinishedProductId")] Projekt projekt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projekt);
                await _context.SaveChangesAsync();

                await CalculateTotalValue(projekt.Id);

                return RedirectToAction(nameof(Index));
            }
            ViewData["SemiFinishedProductId"] = new SelectList(_context.SemiFinishedProducts, "Id", "Id", projekt.SemiFinishedProductId);
            return View(projekt);
        }

        // GET: Projekts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projekts == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekts.FindAsync(id);
            if (projekt == null)
            {
                return NotFound();
            }
            ViewData["SemiFinishedProductId"] = new SelectList(_context.SemiFinishedProducts, "Id", "Id", projekt.SemiFinishedProductId);
            return View(projekt);
        }

        // POST: Projekts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Quantity,SemiFinishedProductId")] Projekt projekt)
        {
            if (id != projekt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projekt);
                    await _context.SaveChangesAsync();

                    await CalculateTotalValue(projekt.Id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjektExists(projekt.Id))
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
            ViewData["SemiFinishedProductId"] = new SelectList(_context.SemiFinishedProducts, "Id", "Id", projekt.SemiFinishedProductId);
            return View(projekt);
        }

        // GET: Projekts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projekts == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekts
                .Include(p => p.SemiFinishedProduct)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projekt == null)
            {
                return NotFound();
            }

            return View(projekt);
        }

        // POST: Projekts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projekts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Projekts' is null.");
            }
            var projekt = await _context.Projekts.FindAsync(id);
            if (projekt != null)
            {
                _context.Projekts.Remove(projekt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjektExists(int id)
        {
            return (_context.Projekts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // Akcja Select
        public async Task<IActionResult> Select(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projekt == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetInt32("SelectedProjectId", projekt.Id);
            return RedirectToAction("Index", "Przedmiots");
        }

        // Metoda obliczająca TotalValue
        private async Task CalculateTotalValue(int projektId)
        {
            var projekt = await _context.Projekts
                .Include(p => p.OperationSets)
                .ThenInclude(os => os.Operations)
                .ThenInclude(o => o.SemiFinishedProduct)
                .FirstOrDefaultAsync(p => p.Id == projektId);

            if (projekt == null) return;

            decimal totalValue = 0;

            foreach (var operationSet in projekt.OperationSets)
            {
                totalValue += operationSet.TotalCost;
                foreach (var operation in operationSet.Operations)
                {
                    totalValue += operation.SemiFinishedProduct.Price * projekt.Quantity;
                }
            }

            projekt.TotalValue = totalValue;
            _context.Update(projekt);
            await _context.SaveChangesAsync();
        }
    }
}
