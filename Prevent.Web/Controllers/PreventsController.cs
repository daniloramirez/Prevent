using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prevent.Web.Data;
using Prevent.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Prevent.Web.Controllers
{
    public class PreventsController : Controller
    {
        private readonly DataContext _context;

        public PreventsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Prevents.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PreventEntity preventEntity = await _context.Prevents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preventEntity == null)
            {
                return NotFound();
            }

            return View(preventEntity);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PreventEntity preventEntity)
        {
            if (ModelState.IsValid)
            {
                preventEntity.Title = preventEntity.Title.ToUpper();
                _context.Add(preventEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(preventEntity);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PreventEntity preventEntity = await _context.Prevents.FindAsync(id);
            if (preventEntity == null)
            {
                return NotFound();
            }
            return View(preventEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PreventEntity preventEntity)
        {
            if (id != preventEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preventEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreventEntityExists(preventEntity.Id))
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
            return View(preventEntity);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PreventEntity preventEntity = await _context.Prevents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preventEntity == null)
            {
                return NotFound();
            }

            return View(preventEntity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            PreventEntity preventEntity = await _context.Prevents.FindAsync(id);
            _context.Prevents.Remove(preventEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreventEntityExists(int id)
        {
            return _context.Prevents.Any(e => e.Id == id);
        }
    }
}