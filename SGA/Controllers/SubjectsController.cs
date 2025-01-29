//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SGA.Models;
//using SGA.Context;

//namespace SGA.Controllers
//{
//    public class SubjectsController : Controller
//    {
//        private readonly AppDbContext _context;
//        public SubjectsController(AppDbContext context) 
//        {
//            _context = context;
//        }
//        // GET: SubjectsController
//        public async Task<ActionResult> Index()
//        {
//            var model = await _context.Subjects.ToListAsync();
//            return View(model);
//        }

//        // GET: SubjectsController/Details/5
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var model = await _context.Students.Include(st => st.Grade).ToListAsync();
//            return View(model);
//        }

//        // GET: SubjectsController/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: SubjectsController/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create(Subject subject)
//        {
//            try
//            {
//                _context.Add(subject);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View("index");
//            }
//        }

//        // GET: SubjectsController/Edit/5
//        public ActionResult Edit(int id)
//        {
//            return View();
//        }

//        // POST: SubjectsController/Edit/5
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

//        // GET: SubjectsController/Delete/5
//        public ActionResult Delete(int id)
//        {
//            return View();
//        }

//        // POST: SubjectsController/Delete/5
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
