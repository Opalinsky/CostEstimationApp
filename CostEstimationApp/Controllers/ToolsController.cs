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
    public class ToolsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tools
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tools.Include(t => t.ToolMaterial);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tools/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tools == null)
            {
                return NotFound();
            }

            var tool = await _context.Tools
                .Include(t => t.ToolMaterial)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tool == null)
            {
                return NotFound();
            }

            return View(tool);
        }

        // GET: Tools/Create
        public IActionResult Create()
        {
            ViewData["ToolMaterialId"] = new SelectList(_context.ToolMaterials, "Id", "Name");
            ViewData["OperationTypeIds"] = new MultiSelectList(_context.OperationTypes, "Id", "Name");
            return View();
        }

        // POST: Tools/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,AmountOfEdges,VitalityPerEdge,ToolMaterialId")] Tool tool, int[] OperationTypeIds)
        {
            if (ModelState.IsValid)
            {
                // Obliczanie CostPerHour
                if (tool.AmountOfEdges > 0 && tool.VitalityPerEdge > 0)
                {
                    tool.CostPerHour = tool.Price / (tool.AmountOfEdges * tool.VitalityPerEdge);
                }

                if (OperationTypeIds != null && OperationTypeIds.Length > 0)
                {
                    foreach (var operationTypeId in OperationTypeIds)
                    {
                        var operationType = await _context.OperationTypes.FindAsync(operationTypeId);
                        if (operationType != null)
                        {
                            tool.OperationTypes.Add(operationType);
                        }
                    }
                }

                _context.Add(tool);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ToolMaterialId"] = new SelectList(_context.ToolMaterials, "Id", "Name", tool.ToolMaterialId);
            ViewData["OperationTypeIds"] = new MultiSelectList(_context.OperationTypes, "Id", "Name", OperationTypeIds);
            return View(tool);
        }

        // GET: Tools/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tools == null)
            {
                return NotFound();
            }

            var tool = await _context.Tools
                .Include(t => t.OperationTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tool == null)
            {
                return NotFound();
            }
            ViewData["ToolMaterialId"] = new SelectList(_context.ToolMaterials, "Id", "Name", tool.ToolMaterialId);
            ViewData["OperationTypeIds"] = new MultiSelectList(_context.OperationTypes, "Id", "Name", tool.OperationTypes.Select(ot => ot.Id));
            return View(tool);
        }

        //// POST: Tools/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,AmountOfEdges,VitalityPerEdge,ToolMaterialId")] Tool tool, int[] OperationTypeIds)
        //{
        //    if (id != tool.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var toolToUpdate = await _context.Tools
        //                .Include(t => t.OperationTypes)
        //                .FirstOrDefaultAsync(t => t.Id == id);

        //            if (toolToUpdate == null)
        //            {
        //                return NotFound();
        //            }

        //            toolToUpdate.Name = tool.Name;
        //            toolToUpdate.Price = tool.Price;
        //            toolToUpdate.AmountOfEdges = tool.AmountOfEdges;
        //            toolToUpdate.VitalityPerEdge = tool.VitalityPerEdge;
        //            toolToUpdate.ToolMaterialId = tool.ToolMaterialId;

        //            // Obliczanie CostPerHour
        //            if (tool.AmountOfEdges > 0 && tool.VitalityPerEdge > 0)
        //            {
        //                toolToUpdate.CostPerHour = (tool.Price ) / (tool.AmountOfEdges * tool.VitalityPerEdge);
        //            }

        //            toolToUpdate.OperationTypes.Clear();
        //            if (OperationTypeIds != null && OperationTypeIds.Length > 0)
        //            {
        //                foreach (var operationTypeId in OperationTypeIds)
        //                {
        //                    var operationType = await _context.OperationTypes.FindAsync(operationTypeId);
        //                    if (operationType != null)
        //                    {
        //                        toolToUpdate.OperationTypes.Add(operationType);
        //                    }
        //                }
        //            }

        //            _context.Update(toolToUpdate);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ToolExists(tool.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ToolMaterialId"] = new SelectList(_context.ToolMaterials, "Id", "Name", tool.ToolMaterialId);
        //    ViewData["OperationTypeIds"] = new MultiSelectList(_context.OperationTypes, "Id", "Name", OperationTypeIds);
        //    return View(tool);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,AmountOfEdges,VitalityPerEdge,ToolMaterialId")] Tool tool, int[] OperationTypeIds)
        {
            if (id != tool.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var toolToUpdate = await _context.Tools
                        .Include(t => t.OperationTypes)
                        .FirstOrDefaultAsync(t => t.Id == id);

                    if (toolToUpdate == null)
                    {
                        return NotFound();
                    }

                    toolToUpdate.Name = tool.Name;
                    toolToUpdate.Price = tool.Price;
                    toolToUpdate.AmountOfEdges = tool.AmountOfEdges;
                    toolToUpdate.VitalityPerEdge = tool.VitalityPerEdge;
                    toolToUpdate.ToolMaterialId = tool.ToolMaterialId;

                    // Obliczanie CostPerHour
                    if (tool.AmountOfEdges > 0 && tool.VitalityPerEdge > 0)
                    {
                        toolToUpdate.CostPerHour = tool.Price / (tool.AmountOfEdges * tool.VitalityPerEdge);
                    }

                    // Aktualizacja operacji typu
                    toolToUpdate.OperationTypes.Clear();
                    if (OperationTypeIds != null && OperationTypeIds.Length > 0)
                    {
                        foreach (var operationTypeId in OperationTypeIds)
                        {
                            var operationType = await _context.OperationTypes.FindAsync(operationTypeId);
                            if (operationType != null)
                            {
                                toolToUpdate.OperationTypes.Add(operationType);
                            }
                        }
                    }

                    _context.Update(toolToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToolExists(tool.Id))
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
            ViewData["ToolMaterialId"] = new SelectList(_context.ToolMaterials, "Id", "Name", tool.ToolMaterialId);
            ViewData["OperationTypeIds"] = new MultiSelectList(_context.OperationTypes, "Id", "Name", OperationTypeIds);
            return View(tool);
        }

        // GET: Tools/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tools == null)
            {
                return NotFound();
            }

            var tool = await _context.Tools
                .Include(t => t.ToolMaterial)
                .Include(t => t.OperationTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tool == null)
            {
                return NotFound();
            }

            return View(tool);
        }

        // POST: Tools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tools == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tools'  is null.");
            }
            var tool = await _context.Tools.FindAsync(id);
            if (tool != null)
            {
                _context.Tools.Remove(tool);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToolExists(int id)
        {
            return (_context.Tools?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
