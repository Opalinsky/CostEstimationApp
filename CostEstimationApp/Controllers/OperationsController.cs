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
    public class OperationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OperationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Operations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Operations.Include(o => o.MRR).Include(o => o.Machine).Include(o => o.SemiFinishedProduct).Include(o => o.Tool).Include(o => o.Worker);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Operations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Operations == null)
            {
                return NotFound();
            }

            var operation = await _context.Operations
                .Include(o => o.MRR)
                .Include(o => o.Machine)
                .Include(o => o.SemiFinishedProduct)
                .Include(o => o.Tool)
                .Include(o => o.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operation == null)
            {
                return NotFound();
            }

            return View(operation);
        }

        // GET: Operations/Create
        public IActionResult Create()
        {
            ViewData["MRRId"] = new SelectList(_context.MRRs, "Id", "Id");
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name");
            ViewData["SemiFinishedProductId"] = new SelectList(_context.SemiFinishedProducts, "Id", "Id");
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Id");
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Id");
            return View();
        }

        // POST: Operations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SemiFinishedProductId,MachineId,WorkerId,ToolId,OperationType,MRRId,CuttingLength,CuttingWidth,CuttingDepth,DrillDiameter,DrillDepth,LengthBeforeOperation,WidthBeforeOperation,HeightBeforeOperation,LengthAfterOperation,WidthAfterOperation,HeightAfterOperation")] Operation operation)
        {
            if (ModelState.IsValid)
            {
                if (operation.OperationType == "Cutting")
                {
                    operation.LengthAfterOperation = operation.LengthBeforeOperation - operation.CuttingLength.GetValueOrDefault();
                    operation.WidthAfterOperation = operation.WidthBeforeOperation - operation.CuttingWidth.GetValueOrDefault();
                    operation.HeightAfterOperation = operation.HeightBeforeOperation - operation.CuttingDepth.GetValueOrDefault();

                    operation.VolumeToRemove = operation.CuttingLength.GetValueOrDefault() * operation.CuttingWidth.GetValueOrDefault() * operation.CuttingDepth.GetValueOrDefault();
                }
                else if (operation.OperationType == "Drilling")
                {
                    operation.HeightAfterOperation = operation.HeightBeforeOperation;
                    operation.LengthAfterOperation = operation.LengthBeforeOperation;
                    operation.WidthAfterOperation = operation.WidthBeforeOperation;
                    
                    var radius = operation.DrillDiameter.GetValueOrDefault() / 2;
                    operation.VolumeToRemove = (decimal)Math.PI * radius * radius * operation.DrillDepth.GetValueOrDefault();
                }
                    operation.MachiningTime = operation.VolumeToRemove / operation.MRRId;
                _context.Add(operation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MRRId"] = new SelectList(_context.MRRs, "Id", "Id", operation.MRRId);
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name", operation.MachineId);
            ViewData["SemiFinishedProductId"] = new SelectList(_context.SemiFinishedProducts, "Id", "Id", operation.SemiFinishedProductId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Id", operation.ToolId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Id", operation.WorkerId);
            return View(operation);
        }

        // GET: Operations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Operations == null)
            {
                return NotFound();
            }

            var operation = await _context.Operations.FindAsync(id);
            if (operation == null)
            {
                return NotFound();
            }
            ViewData["MRRId"] = new SelectList(_context.MRRs, "Id", "Id", operation.MRRId);
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name", operation.MachineId);
            ViewData["SemiFinishedProductId"] = new SelectList(_context.SemiFinishedProducts, "Id", "Id", operation.SemiFinishedProductId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Id", operation.ToolId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Id", operation.WorkerId);
            return View(operation);
        }

        // POST: Operations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SemiFinishedProductId,MachineId,WorkerId,ToolId,OperationType,MRRId,CuttingLength,CuttingWidth,CuttingDepth,DrillDiameter,DrillDepth,LengthBeforeOperation,WidthBeforeOperation,HeightBeforeOperation,LengthAfterOperation,WidthAfterOperation,HeightAfterOperation")] Operation operation)
        {
            if (id != operation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationExists(operation.Id))
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
            ViewData["MRRId"] = new SelectList(_context.MRRs, "Id", "Id", operation.MRRId);
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name", operation.MachineId);
            ViewData["SemiFinishedProductId"] = new SelectList(_context.SemiFinishedProducts, "Id", "Id", operation.SemiFinishedProductId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Id", operation.ToolId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Id", operation.WorkerId);
            return View(operation);
        }

        // GET: Operations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Operations == null)
            {
                return NotFound();
            }

            var operation = await _context.Operations
                .Include(o => o.MRR)
                .Include(o => o.Machine)
                .Include(o => o.SemiFinishedProduct)
                .Include(o => o.Tool)
                .Include(o => o.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operation == null)
            {
                return NotFound();
            }

            return View(operation);
        }

        // POST: Operations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Operations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Operations'  is null.");
            }
            var operation = await _context.Operations.FindAsync(id);
            if (operation != null)
            {
                _context.Operations.Remove(operation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperationExists(int id)
        {
          return (_context.Operations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
