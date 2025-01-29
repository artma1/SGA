using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGA.Models;
using SGA.Context;
using System.ComponentModel;
using AutoMapper;
using SGA.ViewModels;
using static SGA.ViewModels.StudentViewModel;

namespace SGA.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public StudentsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                .Include(s => s.Grades)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            var gradesBySubject = model.Grades
                .Select(ss => new
                {
                    Subject = ss.Subject.ToString(), // Nome da disciplina (converte Enum para string)
                    Grade = ss.Value,               // Nota
                    Attendance = model.Attendance // Frequência geral do aluno
                })
             .ToList();

            ViewBag.Grades = gradesBySubject;
            return View(model);
        }
        // GET: Students/Create
        public IActionResult Create()
        {
            var model = new StudentViewModel
            {
                Grades = Enum.GetValues(typeof(Subjects))
                             .Cast<Subjects>()
                             .Select(subject => new DisciplineGrade
                             {
                                 SubjectName = subject.ToString(),
                                 Grade = 0
                             }).ToList()
            };
            ViewBag.Subjects = Enum.GetValues(typeof(Subjects)).Cast<Subjects>().ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Mapear o ViewModel para o modelo Student
                var student = _mapper.Map<Student>(model);
                _context.Students.Add(student);
                await _context.SaveChangesAsync(); // Salvar para obter o StudentId

                // Mapear as notas para StudentSubjects
                var studentSubjects = _mapper.Map<List<Grade>>(model.Grades);
                foreach (var subject in studentSubjects)
                {
                    subject.StudentId = student.Id; // Associar o ID do estudante
                }

                _context.Grades.AddRange(studentSubjects);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
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
