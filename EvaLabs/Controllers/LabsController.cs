using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvaLabs.Domain.Context;
using EvaLabs.Domain.Entities;

namespace EvaLabs.Controllers
{
    public class LabsController : BaseController
    {
        private readonly IEvaContext _context;

        public LabsController(IEvaContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Labs.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lab = await _context.Labs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lab == null)
            {
                return NotFound();
            }

            return View(lab);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lab lab)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lab);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lab);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lab = await _context.Labs.FindAsync(id);
            if (lab == null)
            {
                return NotFound();
            }
            return View(lab);
        }

        
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Lab lab)
        {
            if (id != lab.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lab);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabExists(lab.Id))
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
            return View(lab);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lab = await _context.Labs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lab == null)
            {
                return NotFound();
            }

            return View(lab);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lab = await _context.Labs.FindAsync(id);
            _context.Labs.Remove(lab);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabExists(int id)
        {
            return _context.Labs.Any(e => e.Id == id);
        }
    }
}