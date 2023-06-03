using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        
        public Category Category { get; set; }
        public DeleteModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void OnGet(int? id)
        {
            if (id != null || id != 0)
            {
                Category = _appDbContext.Categories.Find(id)!;
            }
        }

        public IActionResult OnPost()
        {
            Category category = _appDbContext.Categories.Find(Category.Id)!;
            if (category == null)
            {
                return NotFound();
            }

            _appDbContext.Categories.Remove(category);
            _appDbContext.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToPage("Index");
        }
    }
}
