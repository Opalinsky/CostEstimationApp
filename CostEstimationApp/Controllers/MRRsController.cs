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
    public class MRRsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MRRsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MRRs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MRRs.Include(m => m.Material).Include(m => m.ToolMaterial);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MRRs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MRRs == null)
            {
                return NotFound();
            }

            var mRR = await _context.MRRs
                .Include(m => m.Material)
                .Include(m => m.ToolMaterial)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mRR == null)
            {
                return NotFound();
            }

            return View(mRR);
        }

        // GET: MRRs/Create
        public IActionResult Create()
        {
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name");
            ViewData["ToolMaterialId"] = new SelectList(_context.ToolMaterials, "Id", "Name");
            return View();
        }

        // POST: MRRs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaterialId,ToolMaterialId,Rate,RateFinish")] MRR mRR)
        {
            if (ModelState.IsValid)
            {

                //mRR.Rate = mRR.Rate * 60000;
               // mRR.RateFinish = mRR.RateFinish * 60000;
                _context.Add(mRR);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", mRR.MaterialId);
            ViewData["ToolMaterialId"] = new SelectList(_context.ToolMaterials, "Id", "Name", mRR.ToolMaterialId);
            return View(mRR);
        }

        // GET: MRRs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MRRs == null)
            {
                return NotFound();
            }

            var mRR = await _context.MRRs.FindAsync(id);
            if (mRR == null)
            {
                return NotFound();
            }
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", mRR.MaterialId);
            ViewData["ToolMaterialId"] = new SelectList(_context.ToolMaterials, "Id", "Name", mRR.ToolMaterialId);
            return View(mRR);
        }

        // POST: MRRs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaterialId,ToolMaterialId,Rate,RateFinish")] MRR mRR)
        {
            if (id != mRR.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mRR);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MRRExists(mRR.Id))
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
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", mRR.MaterialId);
            ViewData["ToolMaterialId"] = new SelectList(_context.ToolMaterials, "Id", "Name", mRR.ToolMaterialId);
            return View(mRR);
        }

        // GET: MRRs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MRRs == null)
            {
                return NotFound();
            }

            var mRR = await _context.MRRs
                .Include(m => m.Material)
                .Include(m => m.ToolMaterial)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mRR == null)
            {
                return NotFound();
            }

            return View(mRR);
        }

        // POST: MRRs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MRRs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MRRs'  is null.");
            }
            var mRR = await _context.MRRs.FindAsync(id);
            if (mRR != null)
            {
                _context.MRRs.Remove(mRR);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MRRExists(int id)
        {
          return (_context.MRRs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
