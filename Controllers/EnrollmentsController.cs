using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanguageSchool.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace LanguageSchool.Controllers
{
    [Authorize]
    public class EnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Student>_userManager;

        public EnrollmentsController(ApplicationDbContext context,UserManager<Student> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Enrollments
        
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
var applicationDbContext = _context.Enrollments.Include(e => e.SchoolYears).Include(e => e.Students);
            return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.Enrollments
                    .Include(e => e.SchoolYears)
                    .Include(e => e.Students)
                    .Where(e =>e.StudentId==_userManager.GetUserId(User));
                return View(await applicationDbContext.ToListAsync());
            }
            
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.SchoolYears)
                .Include(e => e.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["SchoolYearId"] = new SelectList(_context.SchoolYears, "Id", "Id");
           // ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SchoolYearId,Result,SertificatId")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                enrollment.RegData=DateTime.Now;
                enrollment.StudentId=_userManager.GetUserId(User);
                _context.Enrollments.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SchoolYearId"] = new SelectList(_context.SchoolYears, "Id", "Id", enrollment.SchoolYearId);
           // ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Id", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["SchoolYearId"] = new SelectList(_context.SchoolYears, "Id", "Id", enrollment.SchoolYearId);
           // ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Id", enrollment.StudentId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SchoolYearId,Result,SertificatId")] Enrollment enrollment)
        {
            if (id != enrollment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    enrollment.RegData=DateTime.Now;
                    enrollment.StudentId = _userManager.GetUserId(User);
                    _context.Enrollments.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.Id))
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
            ViewData["SchoolYearId"] = new SelectList(_context.SchoolYears, "Id", "Name", enrollment.SchoolYearId);
           // ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Id", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.SchoolYears)
                .Include(e => e.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Enrollments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Enrollments'  is null.");
            }
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
          return _context.Enrollments.Any(e => e.Id == id);
        }
    }
}
