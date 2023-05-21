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
    public class AgeTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgeTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AgeTypes
        public async Task<IActionResult> Index()
        {
              return View(await _context.AgeTypes.ToListAsync());
        }

        // GET: AgeTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AgeTypes == null)
            {
                return NotFound();
            }

            var ageType = await _context.AgeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ageType == null)
            {
                return NotFound();
            }

            return View(ageType);
        }

        // GET: AgeTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AgeTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] AgeType ageType)
        {
            if (ModelState.IsValid)
            {
                ageType.RegData=DateTime.Now;
                _context.AgeTypes.Add(ageType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ageType);
        }

        // GET: AgeTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AgeTypes == null)
            {
                return NotFound();
            }

            var ageType = await _context.AgeTypes.FindAsync(id);
            if (ageType == null)
            {
                return NotFound();
            }
            return View(ageType);
        }

        // POST: AgeTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] AgeType ageType)
        {
            if (id != ageType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ageType.RegData = DateTime.Now;
                    _context.AgeTypes.Update(ageType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgeTypeExists(ageType.Id))
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
            return View(ageType);
        }

        // GET: AgeTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AgeTypes == null)
            {
                return NotFound();
            }

            var ageType = await _context.AgeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ageType == null)
            {
                return NotFound();
            }

            return View(ageType);
        }

        // POST: AgeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AgeTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AgeTypes'  is null.");
            }
            var ageType = await _context.AgeTypes.FindAsync(id);
            if (ageType != null)
            {
                _context.AgeTypes.Remove(ageType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgeTypeExists(int id)
        {
          return _context.AgeTypes.Any(e => e.Id == id);
        }
    }
}
