//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SGA.Models;
//using SGA.Context;

//namespace Consumo_veiculos.Controllers
//{
//    public class StudentClassesController : Controller
//    {
//        private readonly AppDbContext _context;
//        public StudentClassesController(AppDbContext context)
//        {
//            _context = context;
//        }
//        // GET: StudentClasses
//        public async Task<ActionResult> Index()
//        {
//            var model = await _context.StudentClasses.ToListAsync();
//            return View(model);
//        }

//        // GET: StudentClasses/Details/5
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }
//            var model = await _context.StudentClasses.Include(st => st.Students).Include(su=> su.Subjects).ToListAsync();
//            return View(model);
//        }

//        // GET: StudentClasses/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: StudentClasses/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create(StudentClass studentClass)
//        {
//            try
//            {
//                _context.Add(studentClass);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: StudentClasses/Edit/5
//        public ActionResult Edit(int id)
//        {
//            return View();
//        }

//        // POST: StudentClasses/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(int id, IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: StudentClasses/Delete/5
//        public ActionResult Delete(int id)
//        {
//            return View();
//        }

//        // POST: StudentClasses/Delete/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Delete(int id, IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }
//    }
//}
