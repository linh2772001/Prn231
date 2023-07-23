using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace PetShop_Project_SWP391.Pages.Customers.Blog
{
    public class ListModel : PageModel
    {
        private readonly ProjectContext dBContext;
        private readonly IConfiguration configuration;
        public ListModel(ProjectContext dBContext)
        {
            this.dBContext = dBContext;

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
        public async Task<IActionResult> OnGet()
        {
            /*if (HttpContext.Session.GetString("PetSession") == null)
                return RedirectToPage("/account/singnin");

            Account = JsonSerializer.Deserialize<Models.Account>(HttpContext.Session.GetString("PetSession"));
            Customer = dBContext.Customers.FirstOrDefault(c => c.CustomerId == Account.CustomerId);
            else
            {
                if (PageNum <= 0 || PageNum is null) PageNum = 1;
                int PageSize = Convert.ToInt32(configuration.GetValue<string>("AppSettings:PageSize"));

                // Kiểm tra giá trị PageSize và gán giá trị mặc định nếu nó bằng 0
                if (PageSize == 0)
                {
                    PageSize = 2; // Giá trị mặc định
                }

                IQueryable<BlogDetail> query = dBContext.BlogDetails;

                if (!string.IsNullOrEmpty(txtSearch))
                {
                    query = query.Where(x => x.PageTitle.Contains(txtSearch.Trim()));
                }

                int TotalProduct = await query.CountAsync();

                // Kiểm tra PageSize để đảm bảo không chia cho 0
                int TotalPage = PageSize > 0 ? (TotalProduct / PageSize) : 0;
                if (TotalProduct % PageSize != 0) TotalPage++;
                ViewData["TotalPage"] = TotalPage;
                ViewData["CurPage"] = PageNum;
                ViewData["txtSearch"] = txtSearch;

                BlogDetails = await query.Skip((int)(PageNum - 1) * PageSize).Take(PageSize).ToListAsync();*/
            if (dBContext.BlogDetails != null)
            {
                BlogDetails = await dBContext.BlogDetails
               .ToListAsync();
            }
            /* if (PageNum <= 0 || PageNum is null) PageNum = 1;
             int PageSize = Convert.ToInt32(configuration.GetValue<string>("AppSettings:PageSize"));

             // Kiểm tra giá trị PageSize và gán giá trị mặc định nếu nó bằng 0
             if (PageSize == 0)
             {
                 PageSize = 2; // Giá trị mặc định
             }

             IQueryable<BlogDetail> query = dBContext.BlogDetails;

             if (!string.IsNullOrEmpty(txtSearch))
             {
                 query = query.Where(x => x.PageTitle.Contains(txtSearch.Trim()));
             }

             int TotalProduct = await query.CountAsync();

             // Kiểm tra PageSize để đảm bảo không chia cho 0
             int TotalPage = PageSize > 0 ? (TotalProduct / PageSize) : 0;
             if (TotalProduct % PageSize != 0) TotalPage++;
             ViewData["TotalPage"] = TotalPage;
             ViewData["CurPage"] = PageNum;
             ViewData["txtSearch"] = txtSearch;

             BlogDetails = await query.Skip((int)(PageNum - 1) * PageSize).Take(PageSize).ToListAsync();*/
            return Page();
        }
    }
}
