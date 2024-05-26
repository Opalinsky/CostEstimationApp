using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CostEstimationApp.Data;
using CostEstimationApp.Models;

namespace CostEstimationApp.Controllers
{
    public class MachinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MachinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Machines
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Machines
                .Include(m => m.MachineType)
                .Include(m => m.OperationTypes);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Machines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Machines == null)
            {
                return NotFound();
            }

            var machine = await _context.Machines
                .Include(m => m.MachineType)
                .Include(m => m.OperationTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (machine == null)
            {
                return NotFound();
            }

            return View(machine);
        }

        // GET: Machines/Create
        public IActionResult Create()
        {
            ViewData["MachineTypeId"] = new SelectList(_context.MachineTypes, "Id", "Typeof");
            ViewData["OperationTypeIds"] = new MultiSelectList(_context.OperationTypes, "Id", "Name");
            return View();
        }

        // POST: Machines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CostPerHour,MachineTypeId")] Machine machine, int[] OperationTypeIds)
        {
            if (ModelState.IsValid)
            {
                if (OperationTypeIds != null)
                {
                    machine.OperationTypes = new List<OperationType>();
                    foreach (var operationTypeId in OperationTypeIds)
                    {
                        var operationType = await _context.OperationTypes.FindAsync(operationTypeId);
                        if (operationType != null)
                        {
                            machine.OperationTypes.Add(operationType);
                        }
                    }
                }

                _context.Add(machine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MachineTypeId"] = new SelectList(_context.MachineTypes, "Id", "Typeof", machine.MachineTypeId);
            ViewData["OperationTypeIds"] = new MultiSelectList(_context.OperationTypes, "Id", "Name", OperationTypeIds);
            return View(machine);
        }

        // GET: Machines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Machines == null)
            {
                return NotFound();
            }

            var machine = await _context.Machines
                .Include(m => m.OperationTypes)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (machine == null)
            {
                return NotFound();
            }

            ViewData["MachineTypeId"] = new SelectList(_context.MachineTypes, "Id", "Typeof", machine.MachineTypeId);
            ViewData["OperationTypeIds"] = new MultiSelectList(_context.OperationTypes, "Id", "Name", machine.OperationTypes.Select(ot => ot.Id));
            return View(machine);
        }

        // POST: Machines/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CostPerHour,MachineTypeId")] Machine machine, int[] OperationTypeIds)
        {
            if (id != machine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var machineToUpdate = await _context.Machines
                        .Include(m => m.OperationTypes)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    if (machineToUpdate == null)
                    {
                        return NotFound();
                    }

                    machineToUpdate.Name = machine.Name;
                    machineToUpdate.CostPerHour = machine.CostPerHour;
                    machineToUpdate.MachineTypeId = machine.MachineTypeId;
                    machineToUpdate.OperationTypes.Clear();

                    if (OperationTypeIds != null)
                    {
                        foreach (var operationTypeId in OperationTypeIds)
                        {
                            var operationType = await _context.OperationTypes.FindAsync(operationTypeId);
                            if (operationType != null)
                            {
                                machineToUpdate.OperationTypes.Add(operationType);
                            }
                        }
                    }

                    _context.Update(machineToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachineExists(machine.Id))
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
            ViewData["MachineTypeId"] = new SelectList(_context.MachineTypes, "Id", "Typeof", machine.MachineTypeId);
            ViewData["OperationTypeIds"] = new MultiSelectList(_context.OperationTypes, "Id", "Name", OperationTypeIds);
            return View(machine);
        }

        // GET: Machines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Machines == null)
            {
                return NotFound();
            }

            var machine = await _context.Machines
                .Include(m => m.MachineType)
                .Include(m => m.OperationTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (machine == null)
            {
                return NotFound();
            }

            return View(machine);
        }

        // POST: Machines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Machines == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Machines' is null.");
            }
            var machine = await _context.Machines.FindAsync(id);
            if (machine != null)
            {
                _context.Machines.Remove(machine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MachineExists(int id)
        {
            return (_context.Machines?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
