using BulkyWeb.Data;
using BulkyWeb.Models;
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
            _appDbContext.Categories.Add(obj);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
    }
}
