using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanguageSchool.Data;

namespace LanguageSchool.Controllers
{
    public class MethodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MethodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Methods
        public async Task<IActionResult> Index()
        {
              return View(await _context.Methods.ToListAsync());
        }

        // GET: Methods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Methods == null)
            {
                return NotFound();
            }

            var @method = await _context.Methods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@method == null)
            {
                return NotFound();
            }

            return View(@method);
        }

        // GET: Methods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Methods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Име")] Method @method)
        {
            if (ModelState.IsValid)
            {
                @method.RegData=DateTime.Now;
                _context.Methods.Add(@method);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@method);
        }

        // GET: Methods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Methods == null)
            {
                return NotFound();
            }

            var @method = await _context.Methods.FindAsync(id);
            if (@method == null)
            {
                return NotFound();
            }
            return View(@method);
        }

        // POST: Methods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Method @method)
        {
            if (id != @method.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    @method.RegData = DateTime.Now;
                    _context.Methods.Update(@method);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MethodExists(@method.Id))
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
            return View(@method);
        }

        // GET: Methods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Methods == null)
            {
                return NotFound();
            }

            var @method = await _context.Methods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@method == null)
            {
                return NotFound();
            }

            return View(@method);
        }

        // POST: Methods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Methods == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Methods'  is null.");
            }
            var @method = await _context.Methods.FindAsync(id);
            if (@method != null)
            {
                _context.Methods.Remove(@method);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MethodExists(int id)
        {
          return _context.Methods.Any(e => e.Id == id);
        }
    }
}
