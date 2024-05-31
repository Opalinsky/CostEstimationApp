using CostEstimationApp.Data;
using CostEstimationApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class OperationSetsController : Controller
{
    private readonly ApplicationDbContext _context;

    public OperationSetsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: OperationSets
    public async Task<IActionResult> Index()
    {
        int? selectedProjectId = HttpContext.Session.GetInt32("SelectedProjectId");
        if (selectedProjectId == null)
        {
            return RedirectToAction("Index", "Projekts");
        }

        var operationSets = await _context.OperationSets
            .Include(os => os.Operations)
            .Where(os => os.ProjektId == selectedProjectId)
            .ToListAsync();

        return View(operationSets);
    }

    // GET: OperationSets/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.OperationSets == null)
        {
            return NotFound();
        }

        var operationSet = await _context.OperationSets
            .Include(os => os.Operations)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (operationSet == null)
        {
            return NotFound();
        }

        return View(operationSet);
    }

    // GET: OperationSets/Create
    public IActionResult Create()
    {
        ViewBag.Operations = new MultiSelectList(_context.Operations, "Id", "Name");
        return View();
    }

    // POST: OperationSets/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] OperationSet operationSet, int[] selectedOperations)
    {
        if (ModelState.IsValid)
        {
            int? selectedProjectId = HttpContext.Session.GetInt32("SelectedProjectId");
            if (selectedProjectId == null)
            {
                return RedirectToAction("Index", "Projekts");
            }

            operationSet.ProjektId = selectedProjectId.Value;
            _context.Add(operationSet);
            await _context.SaveChangesAsync();

            if (selectedOperations != null)
            {
                foreach (var operationId in selectedOperations)
                {
                    var operation = await _context.Operations.FindAsync(operationId);
                    if (operation != null)
                    {
                        operation.OperationSetId = operationSet.Id;
                        _context.Update(operation);
                    }
                }
                await _context.SaveChangesAsync();
            }

            // Oblicz całkowity koszt zestawu operacji
            var selectedOps = _context.Operations
                .Include(o => o.Worker)
                .Where(o => selectedOperations.Contains(o.Id))
                .ToList();

            operationSet.MachineCost = _context.Operations.Where(o => selectedOperations.Contains(o.Id)).Sum(o => o.MachineCost);
            operationSet.ToolCost = _context.Operations.Where(o => selectedOperations.Contains(o.Id)).Sum(o => o.ToolCost);
            operationSet.WorkerCost = _context.Operations.Where(o => selectedOperations.Contains(o.Id)).Sum(o => o.WorkerCost);

            operationSet.TotalCost = operationSet.MachineCost + operationSet.ToolCost + operationSet.WorkerCost;

            _context.Update(operationSet);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        ViewBag.Operations = new MultiSelectList(_context.Operations, "Id", "Name");
        return View(operationSet);
    }

    // GET: OperationSets/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.OperationSets == null)
        {
            return NotFound();
        }

        var operationSet = await _context.OperationSets
            .Include(os => os.Operations)
            .FirstOrDefaultAsync(os => os.Id == id);
        if (operationSet == null)
        {
            return NotFound();
        }

        ViewBag.Operations = new MultiSelectList(_context.Operations, "Id", "Name", operationSet.Operations.Select(o => o.Id));
        return View(operationSet);
    }

    // POST: OperationSets/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] OperationSet operationSet, int[] selectedOperations)
    {
        if (id != operationSet.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var operationSetToUpdate = await _context.OperationSets
                    .Include(os => os.Operations)
                    .FirstOrDefaultAsync(os => os.Id == id);

                if (operationSetToUpdate == null)
                {
                    return NotFound();
                }

                operationSetToUpdate.Operations.Clear();
                foreach (var operationId in selectedOperations)
                {
                    var operation = await _context.Operations.FindAsync(operationId);
                    if (operation != null)
                    {
                        operationSetToUpdate.Operations.Add(operation);
                    }
                }

                // Oblicz koszty dla zestawu operacji
                operationSetToUpdate.TotalCost = operationSetToUpdate.Operations.Sum(o => o.TotalCost);
                operationSetToUpdate.MachineCost = operationSetToUpdate.Operations.Sum(o => o.MachineCost);
                operationSetToUpdate.ToolCost = operationSetToUpdate.Operations.Sum(o => o.ToolCost);
                operationSetToUpdate.WorkerCost = operationSetToUpdate.Operations.Sum(o => o.WorkerCost);

                _context.Update(operationSetToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperationSetExists(operationSet.Id))
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
        ViewBag.Operations = new MultiSelectList(_context.Operations, "Id", "Name", selectedOperations);
        return View(operationSet);
    }

    // GET: OperationSets/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.OperationSets == null)
        {
            return NotFound();
        }

        var operationSet = await _context.OperationSets
            .FirstOrDefaultAsync(m => m.Id == id);
        if (operationSet == null)
        {
            return NotFound();
        }

        return View(operationSet);
    }

    // POST: OperationSets/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.OperationSets == null)
        {
            return Problem("Entity set 'ApplicationDbContext.OperationSets' is null.");
        }
        var operationSet = await _context.OperationSets.FindAsync(id);
        if (operationSet != null)
        {
            _context.OperationSets.Remove(operationSet);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool OperationSetExists(int id)
    {
        return (_context.OperationSets?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
