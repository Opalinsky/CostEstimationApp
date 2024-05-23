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
    public class ToolMaterialsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToolMaterialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ToolMaterials
        public async Task<IActionResult> Index()
        {
              return _context.ToolMaterials != null ? 
                          View(await _context.ToolMaterials.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ToolMaterials'  is null.");
        }

        // GET: ToolMaterials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ToolMaterials == null)
            {
                return NotFound();
            }

            var toolMaterial = await _context.ToolMaterials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toolMaterial == null)
            {
                return NotFound();
            }

            return View(toolMaterial);
        }

        // GET: ToolMaterials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToolMaterials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ToolMaterial toolMaterial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toolMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toolMaterial);
        }

        // GET: ToolMaterials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ToolMaterials == null)
            {
                return NotFound();
            }

            var toolMaterial = await _context.ToolMaterials.FindAsync(id);
            if (toolMaterial == null)
            {
                return NotFound();
            }
            return View(toolMaterial);
        }

        // POST: ToolMaterials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ToolMaterial toolMaterial)
        {
            if (id != toolMaterial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toolMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToolMaterialExists(toolMaterial.Id))
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
            return View(toolMaterial);
        }

        // GET: ToolMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ToolMaterials == null)
            {
                return NotFound();
            }

            var toolMaterial = await _context.ToolMaterials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toolMaterial == null)
            {
                return NotFound();
            }

            return View(toolMaterial);
        }

        // POST: ToolMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ToolMaterials == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ToolMaterials'  is null.");
            }
            var toolMaterial = await _context.ToolMaterials.FindAsync(id);
            if (toolMaterial != null)
            {
                _context.ToolMaterials.Remove(toolMaterial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToolMaterialExists(int id)
        {
          return (_context.ToolMaterials?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
