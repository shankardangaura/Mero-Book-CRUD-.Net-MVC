using MeroBook.DataAccess.Data;
using MeroBook.DataAccess.Repository;
using MeroBook.DataAccess.Repository.IRepository;
using MeroBook.Models;
using MeroBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MeroBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IMeroBookRepository _meroBookRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IMeroBookRepository meroBookRepo, IWebHostEnvironment webHostEnvironment)
        {
            _meroBookRepo = meroBookRepo;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _meroBookRepo.Product.GetAll(includeProperties: "Category").ToList();
            return View(objProductList);
        }
        public IActionResult AddEdit(int? id)
        {
            //IEnumerable<SelectListItem> CategoryList = _meroBookRepo.Category.GetAll().Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.Id.ToString()
            //});
            // ViewBag.CategoryList = CategoryList;
            // ViewData["CategoryList"] = CategoryList;
            ProductVM productVM = new()
            {
                CategoryList = _meroBookRepo.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if(id == null || id == 0)
            {
                //Insert
                return View(productVM);
            }
            else
            {
                //Update
                productVM.Product = _meroBookRepo.Product.Get(u=> u.Id==id);
                return View(productVM);
            }
        }
        [HttpPost]
        public IActionResult AddEdit(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file !=null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if(productVM.Product.Id == 0)
                {
                    _meroBookRepo.Product.Add(productVM.Product);
                }
                else
                {
                    _meroBookRepo.Product.Update(productVM.Product);
                }
                
                _meroBookRepo.Save();
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction("Index", "Product");
            }
            else
            {

                productVM.CategoryList = _meroBookRepo.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
        }

        //public IActionResult Edit(int id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? productFromDb = _meroBookRepo.Product.Get(u => u.Id == id);
        //    // First way of retrieving data
        //    //Category? categoryFromDb = _db.Categories.Find(id);

        //    //Second method to retrieving data
        //    //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=> u.Id==id);

        //    //Third method to retrieving data
        //    //Category? categoryFromDb2 = _db.Categories.Where(u=> u.Id==id).FirstOrDefault();

        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDb);
        //}
        //[HttpPost]
        //public IActionResult Edit(Product obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _meroBookRepo.Product.Update(obj);
        //        _meroBookRepo.Save();
        //        TempData["success"] = "Category Updated Successfully";
        //        return RedirectToAction("Index", "Category");
        //    }
        //    return View(obj);
        //}

        //public IActionResult Delete(int id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    Product? productFromDb = _meroBookRepo.Product.Get(u => u.Id == id);

        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDb);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int? id)
        //{
        //    Product obj = _meroBookRepo.Product.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _meroBookRepo.Product.Remove(obj);
        //    _meroBookRepo.Save();
        //    TempData["success"] = "Category Deleted Successfully";
        //    return RedirectToAction("Index", "Category");
        //}
        #region API Calls

        [HttpGet]
        public JsonResult GetAll()
        {
            try
            {
                List<Product> objProductList = _meroBookRepo.Product.GetAll(includeProperties: "Category").ToList();

                var jsonData = new
                {
                    data = (from t in objProductList
                            select new
                            {
                                Title = t.Title,
                                ISBN = t.ISBN,
                                Price = t.Price,
                                Author = t.Author,
                                CategoryName = t.Category.Name,
                                ProductId = t.Id
                            }).ToArray()
                };

                return new JsonResult(jsonData);

            }
            catch (Exception ex)
            {
                var emptyJsonData = new
                {
                    data = new object[] { }
                };
                return new JsonResult(emptyJsonData) { StatusCode = 500 };
            }
            //List<Product> objProductList = _meroBookRepo.Product.GetAll(includeProperties: "Category").ToList();
            //return Json(new { data = objProductList });




            //var options = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve,
            //    MaxDepth = 64 // Adjust the depth if necessary
            //};

            //var jsonData = new { data = objProductList };

            //return new JsonResult(jsonData, options)
            //{
            //    StatusCode = 200
            //};
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new { error = ex.Message });
            //}
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _meroBookRepo.Product.Get(u=>u.Id == id);
            if(productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            var oldImagePath =
                            Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _meroBookRepo.Product.Remove(productToBeDeleted);
            _meroBookRepo.Save();

            return Json(new { success = true, message = "Error while Deleting" });
        }

        #endregion
    }
}
