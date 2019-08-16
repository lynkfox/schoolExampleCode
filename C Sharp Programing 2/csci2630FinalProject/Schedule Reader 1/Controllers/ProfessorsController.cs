using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Schedule_Reader_1.Models;
using Schedule_Reader_1.ViewModels;


namespace Schedule_Reader_1.Controllers
{
    public class ProfessorsController : Controller
    {
        private readonly DatabaseContext _context;

        public ProfessorsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Professors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Professors.ToListAsync());
        }

        // GET: Professors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors
                .FirstOrDefaultAsync(m => m.ProfessorID == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // GET: Professors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfessorID,Name,Email,Department,ActiveCourseAmount")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(professor);
        }

        // GET: Professors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfessorID,Name,Email,Department,ActiveCourseAmount")] Professor professor)
        {
            if (id != professor.ProfessorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(professor.ProfessorID))
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
            return View(professor);
        }

        // GET: Professors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors
                .FirstOrDefaultAsync(m => m.ProfessorID == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professor = await _context.Professors.FindAsync(id);
            _context.Professors.Remove(professor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professors.Any(e => e.ProfessorID == id);
        }



        public IActionResult Update()
        {

            var viewModel = new ProfessorVM();
            viewModel.ProfList = _context.Professors.Select(q => new ProfName { Id = q.ProfessorID, Name = q.Name }).OrderBy(q => q.Name).ToList();
            viewModel.Departments = _context.Professors.Select(q => q.Department).Distinct().ToList();
            viewModel.Departments.Insert(0, "Select Department...");
            viewModel.ProfList.Insert(0, new ProfName { Id = 999999, Name = "Select from List..." });
            return View(viewModel);
        }

        public IActionResult GetProf(int id)
        {
            Professor prof = _context.Professors.Find(id);

            return Ok(prof);
        }

        public IActionResult CreatePerson(string pName, string pEmail, string pDept)
        {
            var prof = new Professor
            {
                Name = pName,
                Email = pEmail,
                Department = pDept

            };
            _context.Professors.Add(prof);
            int rowsCreated = _context.SaveChanges();

            return Ok(prof.ProfessorID);
        }

        public IActionResult UpdatePerson(int id, string pName, string pEmail, string pDepartment)
        {
            var prof = _context.Professors.Find(id);
            prof.Name = pName;
            prof.Email = pEmail;
            prof.Department = pDepartment;
            _context.Professors.Update(prof);
            int rowsUpdated = _context.SaveChanges();


            return Ok(rowsUpdated);
        }
        public IActionResult DeletePerson(int id)
        {
            var prof = _context.Professors.Find(id);
            _context.Professors.Remove(prof);
            int rowsDeleted = _context.SaveChanges();

            return Ok(rowsDeleted);
        }
    }
}
