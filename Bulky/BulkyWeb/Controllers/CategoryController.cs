using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepsitory;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace BulkyWeb.CategoryControllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objectCategoryList = _unitOfWork.Category.GetAll().ToList();

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

            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index", "Category");
        }

        public IActionResult Edit(int id)
        {
            if(id == 0 || id == 0) 
            {
                return NotFound();
            }
            Category category = _unitOfWork.Category.Get(x => x.Id == id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _unitOfWork.Category.Equals(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category edited successfully";
            return RedirectToAction("Index", "Category");
        }

        public IActionResult Delete(int id)
        {
            if (id == 0 || id == 0)
            {
                return NotFound();
            }

            Category category = _unitOfWork.Category.Get(x => x.Id == id);
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteitPost(int id)
        {
            Category category = _unitOfWork.Category.Get(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
