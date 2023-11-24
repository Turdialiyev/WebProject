using Bulky.DataAccess.Repository.IRepsitory;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.ProductControllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objectProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return View(objectProductList);
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel productMV, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string mainPath = @"image\product";
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, mainPath);

                    if (!String.IsNullOrEmpty(productMV.Product.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, productMV.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productMV.Product.ImageUrl = @"\" + mainPath + @"\" + fileName;
                }

                if (productMV.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productMV.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productMV.Product);
                }

                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                productMV.CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                });

                return View(productMV);
            }
        }

        public IActionResult Upsert(int? id)
        {
            ProductViewModel productViewModel = new()
            {
                CategoryList =
                _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }),
                Product = new Product()
            };

            if (id == 0 || id == null)
            {
                // create
                return View(productViewModel);
            }
            else
            {
                // update
                productViewModel.Product = _unitOfWork.Product.Get(x => x.Id == id);
                return View(productViewModel);
            }
        }
        #region

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objectProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return Json(new { data = objectProductList });
        }

        #endregion

        #region
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            List<Product> objectProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            var productToBeDelete = _unitOfWork.Product.Get(u => u.Id == id);

            if (productToBeDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            if (!String.IsNullOrEmpty(productToBeDelete.ImageUrl))
            {
                //delete the old image
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDelete.ImageUrl!.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _unitOfWork.Product.Remove(productToBeDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message="Deleting successfuly"});
        }

        #endregion
    }
}
