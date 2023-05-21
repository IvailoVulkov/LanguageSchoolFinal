using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanguageSchool.Data;
using Microsoft.AspNetCore.Authorization;

namespace LanguageSchool.Controllers
{

    public class SchoolYearsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolYearsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SchoolYears
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SchoolYears.Include(s => s.Courses).Include(s => s.Methods);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SchoolYears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SchoolYears == null)
            {
                return NotFound();
            }

            var schoolYear = await _context.SchoolYears
                .Include(s => s.Courses)
                .Include(s => s.Methods)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolYear == null)
            {
                return NotFound();
            }

            return View(schoolYear);
        }

        // GET: SchoolYears/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["MethodId"] = new SelectList(_context.Methods, "Id", "Name");
            return View();
        }

        // POST: SchoolYears/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,SchoolYears,DateBegin,DateEnd,Duration,MethodId,Price,Description,BookPrice")] SchoolYear schoolYear)
        {
            if (ModelState.IsValid)
            {
                schoolYear.RegData=DateTime.Now;
                _context.SchoolYears.Add(schoolYear);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", schoolYear.CourseId);
            ViewData["MethodId"] = new SelectList(_context.Methods, "Id", "Name", schoolYear.MethodId);
            return View(schoolYear);
        }

        // GET: SchoolYears/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SchoolYears == null)
            {
                return NotFound();
            }

            var schoolYear = await _context.SchoolYears.FindAsync(id);
            if (schoolYear == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", schoolYear.CourseId);
            ViewData["MethodId"] = new SelectList(_context.Methods, "Id", "Name", schoolYear.MethodId);
            return View(schoolYear);
        }

        // POST: SchoolYears/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,SchoolYears,DateBegin,DateEnd,Duration,MethodId,Price,Description,BookPrice")] SchoolYear schoolYear)
        {
            if (id != schoolYear.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    schoolYear.RegData = DateTime.Now;
                    _context.SchoolYears.Update(schoolYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolYearExists(schoolYear.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", schoolYear.CourseId);
            ViewData["MethodId"] = new SelectList(_context.Methods, "Id", "Name", schoolYear.MethodId);
            return View(schoolYear);
        }

        // GET: SchoolYears/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SchoolYears == null)
            {
                return NotFound();
            }

            var schoolYear = await _context.SchoolYears
                .Include(s => s.Courses)
                .Include(s => s.Methods)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolYear == null)
            {
                return NotFound();
            }

            return View(schoolYear);
        }

        // POST: SchoolYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SchoolYears == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SchoolYears'  is null.");
            }
            var schoolYear = await _context.SchoolYears.FindAsync(id);
            if (schoolYear != null)
            {
                _context.SchoolYears.Remove(schoolYear);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolYearExists(int id)
        {
          return _context.SchoolYears.Any(e => e.Id == id);
        }
    }
}
