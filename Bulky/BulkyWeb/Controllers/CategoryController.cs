using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public CategoryController(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            List<Category> objectCategoryList = _appDbContext.Categories.ToList();

            return View(objectCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj) 
        {
            if(obj.DisplayOrder == 1)
            {
                ModelState.AddModelError("DisplayOrder", "Display Order is not equal 0");
            }
            if(!ModelState.IsValid)
            {
                return View();
            }

            _appDbContext.Categories.Add(obj);
            _appDbContext.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index", "Category");
        }

        public IActionResult Edit(int id)
        {
            if(id == 0 || id == 0) 
            {
                return NotFound();
            }
            Category category = _appDbContext.Categories.Find(id)!;
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _appDbContext.Categories.Update(obj);
            _appDbContext.SaveChanges();
            TempData["success"] = "Category edited successfully";
            return RedirectToAction("Index", "Category");
        }

        public IActionResult Delete(int id)
        {
            if (id == 0 || id == 0)
            {
                return NotFound();
            }

            Category category = _appDbContext.Categories.Find(id)!;
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteitPost(int id)
        {
            Category category = _appDbContext.Categories.Find(id)!;
            if (category == null)
            {
                return NotFound();
            }

            _appDbContext.Categories.Remove(category);
            _appDbContext.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
