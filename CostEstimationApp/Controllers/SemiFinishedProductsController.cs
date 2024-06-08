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
    public class SemiFinishedProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SemiFinishedProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SemiFinishedProducts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SemiFinishedProducts.Include(s => s.Material);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SemiFinishedProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SemiFinishedProducts == null)
            {
                return NotFound();
            }

            var semiFinishedProduct = await _context.SemiFinishedProducts
                .Include(s => s.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (semiFinishedProduct == null)
            {
                return NotFound();
            }

            return View(semiFinishedProduct);
        }

        // GET: SemiFinishedProducts/Create
        public IActionResult Create()
        {
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name");
            return View();
        }

        // POST: SemiFinishedProducts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaterialId,DimensionX,DimensionY,DimensionZ")] SemiFinishedProduct semiFinishedProduct)
        {
            // Pobierz materiał
            var material = await _context.Materials.FirstOrDefaultAsync(m => m.Id == semiFinishedProduct.MaterialId);
            if (material == null)
            {
                ModelState.AddModelError("", "Invalid material.");
                ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", semiFinishedProduct.MaterialId);
                return View(semiFinishedProduct);
            }

            // Oblicz objętość i cenę półfabrykatu
            semiFinishedProduct.Volume = semiFinishedProduct.DimensionX * semiFinishedProduct.DimensionY * semiFinishedProduct.DimensionZ;
            var VolumekgPerm3 = (decimal)semiFinishedProduct.Volume / 1000000000;
            semiFinishedProduct.Price = (decimal)material.Density * VolumekgPerm3 * (decimal)material.PricePerKg;

            if (ModelState.IsValid)
            {
                _context.Add(semiFinishedProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", semiFinishedProduct.MaterialId);
            return View(semiFinishedProduct);
        }

        // GET: SemiFinishedProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SemiFinishedProducts == null)
            {
                return NotFound();
            }

            var semiFinishedProduct = await _context.SemiFinishedProducts.FindAsync(id);
            if (semiFinishedProduct == null)
            {
                return NotFound();
            }
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", semiFinishedProduct.MaterialId);
            return View(semiFinishedProduct);
        }

        // POST: SemiFinishedProducts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaterialId,DimensionX,DimensionY,DimensionZ")] SemiFinishedProduct semiFinishedProduct)
        {
            if (id != semiFinishedProduct.Id)
            {
                return NotFound();
            }

            // Pobierz materiał
            var material = await _context.Materials.FirstOrDefaultAsync(m => m.Id == semiFinishedProduct.MaterialId);
            if (material == null)
            {
                ModelState.AddModelError("", "Invalid material.");
                ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", semiFinishedProduct.MaterialId);
                return View(semiFinishedProduct);
            }

            // Oblicz objętość i cenę półfabrykatu
            semiFinishedProduct.Volume = semiFinishedProduct.DimensionX * semiFinishedProduct.DimensionY * semiFinishedProduct.DimensionZ;
            semiFinishedProduct.Price = (decimal)material.Density * semiFinishedProduct.Volume * (decimal)material.PricePerKg;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(semiFinishedProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SemiFinishedProductExists(semiFinishedProduct.Id))
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
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", semiFinishedProduct.MaterialId);
            return View(semiFinishedProduct);
        }

        // GET: SemiFinishedProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SemiFinishedProducts == null)
            {
                return NotFound();
            }

            var semiFinishedProduct = await _context.SemiFinishedProducts
                .Include(s => s.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (semiFinishedProduct == null)
            {
                return NotFound();
            }

            return View(semiFinishedProduct);
        }

        // POST: SemiFinishedProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SemiFinishedProducts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SemiFinishedProducts' is null.");
            }
            var semiFinishedProduct = await _context.SemiFinishedProducts.FindAsync(id);
            if (semiFinishedProduct != null)
            {
                _context.SemiFinishedProducts.Remove(semiFinishedProduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SemiFinishedProductExists(int id)
        {
            return (_context.SemiFinishedProducts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
