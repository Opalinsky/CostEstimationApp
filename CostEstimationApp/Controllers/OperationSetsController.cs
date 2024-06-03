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
            .Include(os => os.Projekt)  // Include the Projekt for displaying the Projekt name
            .Where(os => os.ProjektId == selectedProjectId)
            .ToListAsync();

        return View(operationSets);
    }

    // GET: OperationSets/Create
    public IActionResult Create()
    {
        int? selectedProjectId = HttpContext.Session.GetInt32("SelectedProjectId");
        if (selectedProjectId == null)
        {
            return RedirectToAction("Index", "Projekts");
        }

        ViewBag.Operations = new MultiSelectList(_context.Operations.Where(o => o.ProjektId == selectedProjectId), "Id", "Name");
        return View();
    }

    // POST: OperationSets/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] OperationSet operationSet, int[] selectedOperations)
    {
        int? selectedProjectId = HttpContext.Session.GetInt32("SelectedProjectId");
        if (selectedProjectId == null)
        {
            return RedirectToAction("Index", "Projekts");
        }

        if (ModelState.IsValid)
        {
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

            // Calculate total cost
            var selectedOps = _context.Operations
                //.Include(o => o.Worker)
                .Where(o => selectedOperations.Contains(o.Id))
                .ToList();

            operationSet.MachineCost = selectedOps.Sum(o => o.MachineCost);
            operationSet.ToolCost = selectedOps.Sum(o => o.ToolCost);
            operationSet.WorkerCost = selectedOps.Sum(o => o.WorkerCost);
            operationSet.TotalCost = operationSet.MachineCost + operationSet.ToolCost + operationSet.WorkerCost;

            _context.Update(operationSet);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        ViewBag.Operations = new MultiSelectList(_context.Operations.Where(o => o.ProjektId == selectedProjectId), "Id", "Name");
        return View(operationSet);
    }

    // GET: OperationSets/Select/5
    public async Task<IActionResult> Select(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var operationSet = await _context.OperationSets
            .FirstOrDefaultAsync(m => m.Id == id);
        if (operationSet == null)
        {
            return NotFound();
        }

        HttpContext.Session.SetInt32("SelectedOperationSetId", operationSet.Id);
        return RedirectToAction("Index", "Operations");
    }

    // Other methods...
}
