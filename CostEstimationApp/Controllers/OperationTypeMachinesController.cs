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
    public class OperationTypeMachinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OperationTypeMachinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OperationTypeMachines
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OperationTypeMachines.Include(o => o.Machine).Include(o => o.OperationType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OperationTypeMachines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OperationTypeMachines == null)
            {
                return NotFound();
            }

            var operationTypeMachine = await _context.OperationTypeMachines
                .Include(o => o.Machine)
                .Include(o => o.OperationType)
                .FirstOrDefaultAsync(m => m.OperationTypeId == id);
            if (operationTypeMachine == null)
            {
                return NotFound();
            }

            return View(operationTypeMachine);
        }

        // GET: OperationTypeMachines/Create
        public IActionResult Create()
        {
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name");
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name");
            return View();
        }

        // POST: OperationTypeMachines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OperationTypeId,MachineId")] OperationTypeMachine operationTypeMachine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operationTypeMachine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name", operationTypeMachine.MachineId);
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name", operationTypeMachine.OperationTypeId);
            return View(operationTypeMachine);
        }

        // GET: OperationTypeMachines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OperationTypeMachines == null)
            {
                return NotFound();
            }

            var operationTypeMachine = await _context.OperationTypeMachines.FindAsync(id);
            if (operationTypeMachine == null)
            {
                return NotFound();
            }
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name", operationTypeMachine.MachineId);
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name", operationTypeMachine.OperationTypeId);
            return View(operationTypeMachine);
        }

        // POST: OperationTypeMachines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OperationTypeId,MachineId")] OperationTypeMachine operationTypeMachine)
        {
            if (id != operationTypeMachine.OperationTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operationTypeMachine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationTypeMachineExists(operationTypeMachine.OperationTypeId))
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
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name", operationTypeMachine.MachineId);
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name", operationTypeMachine.OperationTypeId);
            return View(operationTypeMachine);
        }

        // GET: OperationTypeMachines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OperationTypeMachines == null)
            {
                return NotFound();
            }

            var operationTypeMachine = await _context.OperationTypeMachines
                .Include(o => o.Machine)
                .Include(o => o.OperationType)
                .FirstOrDefaultAsync(m => m.OperationTypeId == id);
            if (operationTypeMachine == null)
            {
                return NotFound();
            }

            return View(operationTypeMachine);
        }

        // POST: OperationTypeMachines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OperationTypeMachines == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OperationTypeMachines'  is null.");
            }
            var operationTypeMachine = await _context.OperationTypeMachines.FindAsync(id);
            if (operationTypeMachine != null)
            {
                _context.OperationTypeMachines.Remove(operationTypeMachine);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperationTypeMachineExists(int id)
        {
          return (_context.OperationTypeMachines?.Any(e => e.OperationTypeId == id)).GetValueOrDefault();
        }
    }
}
