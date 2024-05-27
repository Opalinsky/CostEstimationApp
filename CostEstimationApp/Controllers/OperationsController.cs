﻿using System;
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
            var applicationDbContext = _context.Operations
                .Include(o => o.MRR)
                .Include(o => o.Machine)
                .Include(o => o.OperationType)
                .Include(o => o.SemiFinishedProduct)
                .Include(o => o.Tool)
                .Include(o => o.Worker);
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
                .Include(o => o.OperationType)
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
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name");
            ViewData["SemiFinishedProductId"] = new SelectList(_context.SemiFinishedProducts, "Id", "Id");
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Name");
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "LastName");
            ViewBag.OperationTypeIds = new MultiSelectList(_context.OperationTypes, "Id", "Name");
            return View();
        }

        // POST: Operations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SemiFinishedProductId,MachineId,WorkerId,ToolId,OperationTypeId,MRRId,CuttingLength,CuttingWidth,CuttingDepth,DrillDiameter,DrillDepth,LengthBeforeOperation,WidthBeforeOperation,HeightBeforeOperation,LengthAfterOperation,WidthAfterOperation,HeightAfterOperation,VolumeToRemove,MachiningTime")] Operation operation, int[] OperationTypeIds)
        {
            if (ModelState.IsValid)
            {
                // Pobierz półfabrykat
                var semiFinishedProduct = await _context.SemiFinishedProducts
                    .Include(sp => sp.Material)
                    .FirstOrDefaultAsync(sp => sp.Id == operation.SemiFinishedProductId);
                if (semiFinishedProduct == null)
                {
                    return NotFound();
                }

                // Pobierz narzędzie
                var tool = await _context.Tools
                    .Include(t => t.ToolMaterial)
                    .FirstOrDefaultAsync(t => t.Id == operation.ToolId);
                if (tool == null)
                {
                    return NotFound();
                }

                // Pobierz materiał półfabrykatu
                var material = semiFinishedProduct.Material;
                if (material == null)
                {
                    return NotFound();
                }

                // Pobierz materiał narzędzia
                var toolMaterial = tool.ToolMaterial;
                if (toolMaterial == null)
                {
                    return NotFound();
                }

                // Znajdź MRR na podstawie ToolMaterialId i MaterialId
                var mrr = await _context.MRRs
                    .FirstOrDefaultAsync(m => m.ToolMaterialId == toolMaterial.Id && m.MaterialId == material.Id);
                if (mrr == null)
                {
                    return NotFound();
                }

                operation.MRRId = mrr.Id;

                // Pobierz wymiary z poprzedniej operacji, jeśli istnieje
                var previousOperation = await _context.Operations
                    .Where(o => o.SemiFinishedProductId == operation.SemiFinishedProductId)
                    .OrderByDescending(o => o.Id)
                    .FirstOrDefaultAsync();

                if (previousOperation != null)
                {
                    operation.LengthBeforeOperation = previousOperation.LengthAfterOperation;
                    operation.WidthBeforeOperation = previousOperation.WidthAfterOperation;
                    operation.HeightBeforeOperation = previousOperation.HeightAfterOperation;
                }
                else
                {
                    operation.LengthBeforeOperation = semiFinishedProduct.DimensionX;
                    operation.WidthBeforeOperation = semiFinishedProduct.DimensionY;
                    operation.HeightBeforeOperation = semiFinishedProduct.DimensionZ;
                }

                // Obliczenia dla typu operacji
                if (operation.OperationTypeId == (int)OperationTypes.Cutting)
                {
                    operation.LengthAfterOperation = operation.LengthBeforeOperation - operation.CuttingLength.GetValueOrDefault();
                    operation.WidthAfterOperation = operation.WidthBeforeOperation - operation.CuttingWidth.GetValueOrDefault();
                    operation.HeightAfterOperation = operation.HeightBeforeOperation - operation.CuttingDepth.GetValueOrDefault();

                    operation.VolumeToRemove = operation.CuttingLength.GetValueOrDefault() * operation.CuttingWidth.GetValueOrDefault() * operation.CuttingDepth.GetValueOrDefault();
                }
                else if (operation.OperationTypeId == (int)OperationTypes.Drilling)
                {
                    var radius = operation.DrillDiameter.GetValueOrDefault() / 2;
                    operation.VolumeToRemove = (decimal)Math.PI * radius * radius * operation.DrillDepth.GetValueOrDefault();
                    operation.LengthAfterOperation = operation.LengthBeforeOperation;
                    operation.WidthAfterOperation = operation.WidthBeforeOperation;
                    operation.HeightAfterOperation = operation.HeightBeforeOperation;
                }

                operation.MachiningTime = operation.VolumeToRemove / mrr.Rate;

                _context.Add(operation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MRRId"] = new SelectList(_context.MRRs, "Id", "Id", operation.MRRId);
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name", operation.MachineId);
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name", operation.OperationTypeId);
            ViewData["SemiFinishedProductId"] = new SelectList(_context.SemiFinishedProducts, "Id", "Id", operation.SemiFinishedProductId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Name", operation.ToolId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "LastName", operation.WorkerId);
            ViewBag.OperationTypeIds = new MultiSelectList(_context.OperationTypes, "Id", "Name");
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
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name", operation.OperationTypeId);
            ViewData["SemiFinishedProductId"] = new SelectList(_context.SemiFinishedProducts, "Id", "Id", operation.SemiFinishedProductId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Name", operation.ToolId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "LastName", operation.WorkerId);
            ViewBag.OperationTypeIds = new MultiSelectList(_context.OperationTypes, "Id", "Name");
            return View(operation);
        }

        // POST: Operations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SemiFinishedProductId,MachineId,WorkerId,ToolId,OperationTypeId,MRRId,CuttingLength,CuttingWidth,CuttingDepth,DrillDiameter,DrillDepth,LengthBeforeOperation,WidthBeforeOperation,HeightBeforeOperation,LengthAfterOperation,WidthAfterOperation,HeightAfterOperation,VolumeToRemove,MachiningTime")] Operation operation)
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
            ViewData["OperationTypeId"] = new SelectList(_context.OperationTypes, "Id", "Name", operation.OperationTypeId);
            ViewData["SemiFinishedProductId"] = new SelectList(_context.SemiFinishedProducts, "Id", "Id", operation.SemiFinishedProductId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Name", operation.ToolId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "LastName", operation.WorkerId);
            ViewBag.OperationTypeIds = new MultiSelectList(_context.OperationTypes, "Id", "Name");
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
                .Include(o => o.OperationType)
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
                return Problem("Entity set 'ApplicationDbContext.Operations' is null.");
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
