using Book.DataAccess.Data;
using Book.DataAccess.Repository;
using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitofwork _unitofwork;
        public CategoryController(ApplicationDbContext db, IUnitofwork unitofwork)
        {
            _db = db;
            _unitofwork = unitofwork;
        }
        public IActionResult Index()
        {
            List<Category> Objectdb = _unitofwork.categoryunit.GetAll().ToList();
            return View(Objectdb);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.categoryunit.Add(obj);
                _unitofwork.save();
                TempData["success"] = "Successfully created";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            Category objofcat = _unitofwork.categoryunit.Get(c => c.Id == id);
            if (objofcat == null)
            {
                return NotFound();
            }
            return View(objofcat);
        }
        [HttpPost]
        public IActionResult Edit(Category objj)
        {
            if (objj == null)
            {
                return NotFound();
            }
            _unitofwork.categoryunit.update(objj);
            _unitofwork.save();
            TempData["success"] = "Successfully updated";
            return RedirectToAction("Index", "Category");
        }
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            Category objofcats = _unitofwork.categoryunit.Get(c => c.Id == id);
            if (objofcats == null)
            {
                return NotFound();
            }
            return View(objofcats);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(Category obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            _unitofwork.categoryunit.Remove(obj);
            _unitofwork.save();
            TempData["success"] = "Successfully Deleted";
            return RedirectToAction("Index", "Category");
        }
    }
}
