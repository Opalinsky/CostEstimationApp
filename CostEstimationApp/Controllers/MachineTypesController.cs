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
    public class MachineTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MachineTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MachineTypes
        public async Task<IActionResult> Index()
        {
              return _context.MachineTypes != null ? 
                          View(await _context.MachineTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.MachineTypes'  is null.");
        }

        // GET: MachineTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MachineTypes == null)
            {
                return NotFound();
            }

            var machineType = await _context.MachineTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (machineType == null)
            {
                return NotFound();
            }

            return View(machineType);
        }

        // GET: MachineTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MachineTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Typeof,AdditionalTime,AuxiliaryTime")] MachineType machineType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(machineType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(machineType);
        }

        // GET: MachineTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MachineTypes == null)
            {
                return NotFound();
            }

            var machineType = await _context.MachineTypes.FindAsync(id);
            if (machineType == null)
            {
                return NotFound();
            }
            return View(machineType);
        }

        // POST: MachineTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Typeof,AdditionalTime,AuxiliaryTime")] MachineType machineType)
        {
            if (id != machineType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(machineType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachineTypeExists(machineType.Id))
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
            return View(machineType);
        }

        // GET: MachineTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MachineTypes == null)
            {
                return NotFound();
            }

            var machineType = await _context.MachineTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (machineType == null)
            {
                return NotFound();
            }

            return View(machineType);
        }

        // POST: MachineTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MachineTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MachineTypes'  is null.");
            }
            var machineType = await _context.MachineTypes.FindAsync(id);
            if (machineType != null)
            {
                _context.MachineTypes.Remove(machineType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MachineTypeExists(int id)
        {
          return (_context.MachineTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
