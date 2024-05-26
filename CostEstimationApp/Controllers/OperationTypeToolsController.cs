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
    public class OperationTypeToolsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OperationTypeToolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OperationTypeTools
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OperationTypeTools.Include(o => o.OperationType).Include(o => o.Tool);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OperationTypeTools/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OperationTypeTools == null)
            {
                return NotFound();
            }

            var operationTypeTool = await _context.OperationTypeTools
                .Include(o => o.OperationType)
                .Include(o => o.Tool)
                .FirstOrDefaultAsync(m => m.OperationTypeId == id);
            if (operationTypeTool == null)
            {
                return NotFound();
            }

            return View(operationTypeTool);
        }

        // GET: OperationTypeTools/Create
        public IActionResult Create()
        {
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name");
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Id");
            return View();
        }

        // POST: OperationTypeTools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OperationTypeId,ToolId")] OperationTypeTool operationTypeTool)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operationTypeTool);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name", operationTypeTool.OperationTypeId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Id", operationTypeTool.ToolId);
            return View(operationTypeTool);
        }

        // GET: OperationTypeTools/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OperationTypeTools == null)
            {
                return NotFound();
            }

            var operationTypeTool = await _context.OperationTypeTools.FindAsync(id);
            if (operationTypeTool == null)
            {
                return NotFound();
            }
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name", operationTypeTool.OperationTypeId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Id", operationTypeTool.ToolId);
            return View(operationTypeTool);
        }

        // POST: OperationTypeTools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OperationTypeId,ToolId")] OperationTypeTool operationTypeTool)
        {
            if (id != operationTypeTool.OperationTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operationTypeTool);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationTypeToolExists(operationTypeTool.OperationTypeId))
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
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name", operationTypeTool.OperationTypeId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Id", operationTypeTool.ToolId);
            return View(operationTypeTool);
        }

        // GET: OperationTypeTools/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OperationTypeTools == null)
            {
                return NotFound();
            }

            var operationTypeTool = await _context.OperationTypeTools
                .Include(o => o.OperationType)
                .Include(o => o.Tool)
                .FirstOrDefaultAsync(m => m.OperationTypeId == id);
            if (operationTypeTool == null)
            {
                return NotFound();
            }

            return View(operationTypeTool);
        }

        // POST: OperationTypeTools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OperationTypeTools == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OperationTypeTools'  is null.");
            }
            var operationTypeTool = await _context.OperationTypeTools.FindAsync(id);
            if (operationTypeTool != null)
            {
                _context.OperationTypeTools.Remove(operationTypeTool);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperationTypeToolExists(int id)
        {
          return (_context.OperationTypeTools?.Any(e => e.OperationTypeId == id)).GetValueOrDefault();
        }
    }
}
