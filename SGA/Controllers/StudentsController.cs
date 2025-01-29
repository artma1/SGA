using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGA.Models;
using SGA.Context;

namespace SGA.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;
        public StudentsController(AppDbContext context)
        {
            _context = context;
        }
        // GET: Students
        public async Task<ActionResult> Index()
        {
            var model = await _context.Students.ToListAsync();
           // var totalAverage = await _context.Students.Include(g => g.Grades).ToListAsync();
            // ViewBag.TotalGradesAverage = totalAverage;
            return View(model);
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Students
                .Include(s => s.StudentSubjects)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            var gradesBySubject = model.StudentSubjects
                .Select(ss => new
             {
            Subject = ss.Subject.ToString(), // Nome da disciplina (converte Enum para string)
            Grade = ss.Grade,               // Nota
            Attendance = model.Attendance // Frequência geral do aluno
            })
             .ToList();

            ViewBag.Grades = gradesBySubject;
            return View(model);
        }

        // GET: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);

                var studentSubjects = Enum.GetValues(typeof(SubjectEnum))
                    .Cast<SubjectEnum>()
                    .Select(subject => new StudentSubject
                    {
                        Student = student,
                        Subject = subject,
                        Grade = 0,
                        Attendance = 0
                    });

                _context.StudentSubjects.AddRange(studentSubjects);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Students/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
