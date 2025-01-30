using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGA.Models;
using SGA.Context;
using System.ComponentModel;
using AutoMapper;
using SGA.ViewModels;
using static SGA.ViewModels.StudentViewModel;
using Microsoft.IdentityModel.Tokens;

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
            var students = await _context.Students.ToListAsync();
            var model = _mapper.Map<List<StudentViewModel>>(students);
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
            
           // ViewBag.Subjects = new List<string> { "Cálculo-1", "Economia-2", "Geometria-3", "Filosofia-4", "Projetos-5" };
            return View(new StudentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    ModelState.AddModelError("Name", "O nome não pode ser nulo ou vazio.");
                    return View(model);
                }

                var student = new StudentViewModel()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Attendance = model.Attendance,
                    Grades = model.Grades
                };

                _context.Students.Add(model);
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
