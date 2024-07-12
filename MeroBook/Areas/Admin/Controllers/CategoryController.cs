using MeroBook.DataAccess.Data;
using MeroBook.DataAccess.Repository;
using MeroBook.DataAccess.Repository.IRepository;
using MeroBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeroBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IMeroBookRepository _meroBookRepo;
        public CategoryController(IMeroBookRepository meroBookRepo)
        {
            _meroBookRepo = meroBookRepo;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _meroBookRepo.Category.GetAll().ToList();
            return View(objCategoryList);
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
                _meroBookRepo.Category.Add(obj);
                _meroBookRepo.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index", "Category");
            }
            return View(obj);
        }

        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _meroBookRepo.Category.Get(u => u.Id == id);
            // First way of retrieving data
            //Category? categoryFromDb = _db.Categories.Find(id);

            //Second method to retrieving data
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=> u.Id==id);

            //Third method to retrieving data
            //Category? categoryFromDb2 = _db.Categories.Where(u=> u.Id==id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _meroBookRepo.Category.Update(obj);
                _meroBookRepo.Save();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index", "Category");
            }
            return View(obj);
        }

        public IActionResult Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _meroBookRepo.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category obj = _meroBookRepo.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _meroBookRepo.Category.Remove(obj);
            _meroBookRepo.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
