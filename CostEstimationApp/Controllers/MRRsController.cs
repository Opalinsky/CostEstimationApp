using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CostEstimationApp.Data;
using CostEstimationApp.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        // GET: MRRs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mRR = await _context.MRRs
                .Include(m => m.Material)
                .Include(m => m.Tool)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mRR == null)
            {
                return NotFound();
            }

            return View(mRR);
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
        public async Task<IActionResult> Create([Bind("MaterialId,ToolId,Rate")] MRR mRR)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mRR);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Jeśli ModelState jest nieprawidłowy (są błędy walidacji)
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage); // Lub użyj loggera
                }
                ViewBag.ErrorMessages = errors.Select(e => e.ErrorMessage).ToList();
            }
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", mRR.MaterialId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Name", mRR.ToolId);
            return View(mRR);
        }

        // GET: MRRs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var mRR = await _context.MRRs.FindAsync(id);
            if (mRR == null)
            {
                return NotFound();
            }
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", mRR.MaterialId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Name", mRR.ToolId);
            return View(mRR);
        }

        // POST: MRRs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaterialId,ToolId,Rate")] MRR mRR)
        {
            if (id != mRR.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mRR);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MRRExists(mRR.Id))
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
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", mRR.MaterialId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "Name", mRR.ToolId);
            return View(mRR);
        }

        // GET: MRRs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mRR = await _context.MRRs
                .Include(m => m.Material)
                .Include(m => m.Tool)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mRR == null)
            {
                return NotFound();
            }

            return View(mRR);
        }

        // POST: MRRs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mRR = await _context.MRRs.FindAsync(id);
            if (mRR != null)
            {
                _context.MRRs.Remove(mRR);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MRRExists(int id)
        {
            return _context.MRRs.Any(e => e.Id == id);
        }
    }
}
