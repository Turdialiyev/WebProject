using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categorirs
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        public List<Category>? CategoryList { get; set; }
        public IndexModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void OnGet()
        {
            CategoryList = _appDbContext.Categories.ToList();
        }
    }
}
