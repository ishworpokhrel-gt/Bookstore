using Book.DataAccess.Data;
using Book.DataAccess.Repository;
using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Book.Models.Viewmodel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Collections.Generic;

namespace Bookstore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitofwork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext db,IUnitofwork unitofwork,IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _unitofwork = unitofwork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> Objectdb = _unitofwork.productunit.GetAll(IncludeProperty:"Category").ToList();
            return View(Objectdb);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitofwork.categoryunit.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),

                }),
                product = new Product()
            };
            if(id==null || id==0)
            {
                return View(productVM);
            }
            else
            {
                productVM.product = _unitofwork.productunit.Get(u => u.Id == id);
                return View(productVM);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file!=null)
                {
                    string FileName=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                    string productpath=Path.Combine(wwwRootPath,@"images\product");

                    if (!string.IsNullOrEmpty(obj.product.ImgUrl))
                    {
                        var oldpath = Path.Combine(wwwRootPath, obj.product.ImgUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldpath))
                        {
                            System.IO.File.Delete(oldpath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productpath, FileName), FileMode.Create))
                    {
                       file.CopyTo(fileStream);
                    }

                   obj.product.ImgUrl = @"\images\product\"+FileName;
                }
                if (obj.product.Id == 0)
                {
                    _unitofwork.productunit.Add(obj.product);
                }
                else
                {
                    _unitofwork.productunit.update(obj.product);
                }
                
                _unitofwork.save();
                TempData["success"] = "Successfully created";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                obj.CategoryList = _unitofwork.categoryunit.GetAll().Select(u=>new SelectListItem
                {
                    Text= u.Name,
                    Value=u.Id.ToString()
                });
                return View(obj);
            }
           
        }

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            Product objofprods = _unitofwork.productunit.Get(a => a.Id == id);
            if (objofprods == null)
            {
                return NotFound();
            }
            return View(objofprods);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(Product obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            _unitofwork.productunit.Remove(obj);
            _unitofwork.save();
           TempData["success"] = "Successfully Deleted";
            return RedirectToAction("Index", "Product");
        }
    }
}
