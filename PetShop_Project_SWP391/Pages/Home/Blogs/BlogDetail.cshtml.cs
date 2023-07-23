using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
/*using PetShop_Project_SWP391.Models;*/

namespace PetShop_Project_SWP391.Pages.Customers.Blogs
{
    public class BlogDetailModel : PageModel
    {
        private readonly ProjectContext dBContext;
        private readonly IConfiguration configuration;
        public BlogDetailModel(ProjectContext dBContext, IConfiguration configuration)
        {
            this.dBContext = dBContext;
            this.configuration = configuration;
        }
            [BindProperty]
        public List<BusinessObject.Models.Account> Accounts { get; set; }

        [BindProperty]
        public @BusinessObject.Models.Account Account { get; set; }
        [BindProperty]
        public @BusinessObject.Models.BlogDetail BlogDetail { get; set; }
        [BindProperty]
        public List<BusinessObject.Models.BlogDetail> BlogDetails { get; set; }
        [BindProperty]
        public List<BusinessObject.Models.Customer> Customers { get; set; }
        [BindProperty]
        public Customer Customer { get; set; }
        [BindProperty]
        public @BusinessObject.Models.Employee Employee { get; set; }
        [BindProperty]
        public List<BusinessObject.Models.Employee> Employees { get; set; }
     
        public async Task<IActionResult> OnGet(int id)
        {
            var blogdetail = await dBContext.BlogDetails.FirstOrDefaultAsync(m => m.BlogId == id);
            if (blogdetail == null)
            {
                return NotFound();
            }
            BlogDetail = blogdetail;

            // Get the three latest blogs
            //3 bài mới nhất 
            BlogDetails = await dBContext.BlogDetails
                .OrderByDescending(blog => blog.PublishedDate)
                .Take(3)
                .ToListAsync();
            /*BlogDetails = blogdetail.PageTitle.Take(3).ToList();*/
           

            return Page();
        }

        private bool BlogDetailExists(int id)
        {
            return (dBContext.BlogDetails?.Any(e => e.BlogId == id)).GetValueOrDefault();
        }
    }
}
