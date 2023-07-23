using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
/*using PetShop_Project_SWP391.Models;*/

namespace PetShop_Project_SWP391.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly ProjectContext dBContext;
        public IndexModel(ProjectContext dBContext)
        {
            this.dBContext = dBContext;
        }
        [BindProperty]
        public List<Category> Category { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Category = await dBContext.Categories.ToListAsync();

            return Page();
        }
    }
}
