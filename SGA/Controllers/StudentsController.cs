using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGA.Models;
using SGA.Context;
using AutoMapper;
using SGA.ViewModels;
using static SGA.ViewModels.StudentViewModel;

namespace SGA.Controllers
{
  public class StudentsController : Controller
  {
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private Subjects subject;

    public StudentsController(AppDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }
    // GET: Students
    public async Task<ActionResult> Index()
    {
      var students = await _context.Students
        .OrderByDescending(st => st.AverageGrades)
        .ToListAsync();
      var model = _mapper.Map<List<StudentViewModel>>(students);
      return View(model);
    }

    public async Task<ActionResult> IndexAttendance()
    {
      var students = await _context.Students.Where(st => st.Attendance < 75)
        .OrderByDescending(st => st.AverageGrades)
        .ToListAsync();
      var model = _mapper.Map<List<StudentViewModel>>(students);
      return View(model);
    }

    public async Task<ActionResult> IndexAboveAverage()
    {
      double totalAverage = await _context.Students.AverageAsync(st => st.AverageGrades);
      var students = await _context.Students.Where(st => st.AverageGrades > totalAverage)
        .OrderByDescending(st => st.AverageGrades)
        .ToListAsync();
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

      //var gradesBySubject = model.Grades
      //    .Select(ss => new
      //    {
      //      Subject = ss.Subject.ToString(), // Nome da disciplina (converte Enum para string)
      //      Grade = ss.Value,               // Nota
      //      Attendance = model.Attendance // Frequência geral do aluno
      //    })
      // .ToList();

      //ViewBag.Grades = gradesBySubject;
      return View(model);
    }
    // GET: Students/Create
    public IActionResult Create()
    {
      //var model = new StudentViewModel
      //{
      //  Grades = Enum.GetValues(typeof(Subjects))
      //                 .Cast<Subjects>()
      //                 .Select(subject => new Grade
      //                 {
      //                   SubjectId = subject,
      //                 }).ToList()
      //};

      ViewBag.Subjects = Enum.GetValues(typeof(Subjects)).Cast<Subjects>().ToList();

      // ViewBag.Subjects = new List<string> { "Cálculo-1", "Economia-2", "Geometria-3", "Filosofia-4", "Projetos-5" };
      return View(new StudentViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StudentViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }

      var grades = new List<Grade>();
      int enumId = 0;
      int AverageGrade = 0;
      foreach (Subjects subjects in Enum.GetValues(typeof(Subjects)))
      {
        enumId++;
          var oneGrade = new Grade
          {
            StudentId = model.Id,
            SubjectId = enumId,
            Value = model.GradeValues[enumId - 1],

          };
        AverageGrade += oneGrade.Value;
        grades.Add(oneGrade);
      }

      var student = new Student()
      {
        Id = model.Id,
        Name = model.Name,
        Attendance = model.Attendance,
        Grades = grades,
        AverageGrades = AverageGrade/5,
      };

      _context.Students.Add(student);
      await _context.SaveChangesAsync();

      return RedirectToAction(nameof(Index));
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
