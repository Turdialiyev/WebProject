using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepsitory;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.ProductControllers
{
    
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> objectProductList = _unitOfWork.Product.GetAll().ToList();

            return View(objectProductList);
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> categoryList =  
                _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                });

            ViewBag.CategoryList = categoryList;

            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj) 
        {

            _unitOfWork.Product.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Edit(int id)
        {
            if(id == 0 || id == 0) 
            {
                return NotFound();
            }
            Product Product = _unitOfWork.Product.Get(x => x.Id == id);
            return View(Product);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _unitOfWork.Product.Equals(obj);
            _unitOfWork.Product.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product edited successfully";
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Delete(int id)
        {
            if (id == 0 || id == 0)
            {
                return NotFound();
            }

            Product Product = _unitOfWork.Product.Get(x => x.Id == id);
            return View(Product);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteitPost(int id)
        {
            Product Product = _unitOfWork.Product.Get(x => x.Id == id);
            if (Product == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(Product);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index", "Product");
        }
    }
}
