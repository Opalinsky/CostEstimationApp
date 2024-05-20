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
            var applicationDbContext = _context.MRRs.Include(m => m.Material).Include(m => m.Tool);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MRRs/Create
        public IActionResult Create()
        {
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name");
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Name");
            return View();
        }

        // POST: MRRs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaterialId,ToolId,Rate")] MRR mRR)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mRR);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If ModelState is invalid, capture the errors and re-populate ViewData for dropdowns
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewBag.ErrorMessages = errors.Select(e => e.ErrorMessage).ToList();

            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", mRR.MaterialId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Name", mRR.ToolId);
            return View(mRR);
        }

        // Other actions...

        // Helper method to check if MRR exists
        private bool MRRExists(int id)
        {
            return _context.MRRs.Any(e => e.Id == id);
        }
    }
}
