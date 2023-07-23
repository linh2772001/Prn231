/*using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
*//*using PetShop_Project_SWP391.Models;*//*
using System.Text.Json;

namespace PetShop_Project_SWP391.Pages.Manager.Products
{

    [Authorize(Roles = "1")]
    public class IndexModel : PageModel
    {
        private readonly ProjectContext _context;
        private readonly IConfiguration configuration;
        public IndexModel(ProjectContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }
        public List<BusinessObject.Models.Product> Products { get; set; } = default!;
        [BindProperty]
        public @BusinessObject.Models.Account Account { get; set; }
        [BindProperty]
        public PictureProduct1 pictureProduct { get; set; }
        [BindProperty]
        public Customer Customer { get; set; }
        [BindProperty]
        public Category Categories { get; set; }
        public BusinessObject.Models.Employee Employee { get; set; }
        public string importErr { get; set; }
        public async Task<IActionResult> OnGet(int? PageNum, string? txtSearch)
        {
            if (HttpContext.Session.GetString("PetSession") == null)
            {
                return RedirectToPage("/account/singnin");
            }
            else
            {
                if (PageNum <= 0 || PageNum is null) PageNum = 1;
                int PageSize = Convert.ToInt32(configuration.GetValue<string>("AppSettings:PageSize"));

                // Kiểm tra giá trị PageSize và gán giá trị mặc định nếu nó bằng 0
                if (PageSize == 0)
                {
                    PageSize = 10; // Giá trị mặc định
                }

                //Accounts = await dBContext.Accounts.Where(x => x.Role == 3).Include(x => x.Shipper).ToListAsync();
                Products = await _context.Products.Include(x => x.Category)
                    .Include(x=>x.Pictures).ToListAsync();

                if (txtSearch != null) Products = Products.Where(x => x.ProductName.Contains(txtSearch.Trim())).ToList();

                int TotalProduct = Products.Count;

                // Kiểm tra PageSize để đảm bảo không chia cho 0
                int TotalPage = PageSize > 0 ? (TotalProduct / PageSize) : 0;
                if (TotalProduct % PageSize != 0) TotalPage++;
                ViewData["TotalPage"] = TotalPage;
                ViewData["CurPage"] = PageNum;
                ViewData["txtSearch"] = txtSearch;

                Products = Products.Skip((int)(((PageNum - 1) * PageSize + 1) - 1)).Take(PageSize).ToList();

                return Page();
            }
        }
        public IActionResult OnGetActive(int? id)
        {
            if (id == null) return Redirect("~/Manager/Products/index");

            BusinessObject.Models.Product product = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (product == null) return Redirect("~/Manager/Products/index");

            if (product.Status == true)
            {
                product.Status = false;
                product.Discontinued = true;
                //product.QuantityPerUnit = 0;
            }
            else
            {
                product.Status = true;
                product.Discontinued = false;
            }
            _context.SaveChanges();

            return Redirect("~/Manager/Products/index");
        }
    }
}
*/
using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
/*using PetShop_Project_SWP391.Models;*/
using System.Text.Json;

namespace PetShop_Project_SWP391.Pages.Manager.Products
{
    // Yêu cầu phải đăng nhập và có vai trò là "1" để truy cập trang
    [Authorize(Roles = "1")]
    public class IndexModel : PageModel
    {
        private readonly ProjectContext _context;
        private readonly IConfiguration configuration;
        public IndexModel(ProjectContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        public List<BusinessObject.Models.Product> Products { get; set; } = default!;
        [BindProperty]
        public @BusinessObject.Models.Account Account { get; set; }
        [BindProperty]
        public PictureProduct1 pictureProduct { get; set; }
        [BindProperty]
        public Customer Customer { get; set; }
        [BindProperty]
        public Category Categories { get; set; }
        public BusinessObject.Models.Employee Employee { get; set; }
        public string importErr { get; set; }

        public async Task<IActionResult> OnGet(int? PageNum, string? txtSearch)
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa. Nếu chưa, chuyển hướng đến trang đăng nhập.
            if (HttpContext.Session.GetString("PetSession") == null)
            {
                return RedirectToPage("/account/singnin");
            }
            else
            {
                // Kiểm tra và xử lý trang hiện tại (PageNum) và kích thước trang (PageSize)
                if (PageNum == null || PageNum <= 0) PageNum = 1;
                int PageSize = Convert.ToInt32(configuration.GetValue<string>("AppSettings:PageSize"));

                // Kiểm tra giá trị PageSize và gán giá trị mặc định nếu nó bằng 0
                if (PageSize == 0)
                {
                    PageSize = 10; // Giá trị mặc định
                }

                // Lấy danh sách sản phẩm từ cơ sở dữ liệu, bao gồm các thông tin liên quan
                Products = await _context.Products.Include(x => x.Category)
                    .Include(x => x.Pictures).ToListAsync();

                // Tìm kiếm sản phẩm theo từ khóa (nếu có)
                if (txtSearch != null) Products = Products.Where(x => x.ProductName.Contains(txtSearch.Trim())).ToList();

                int TotalProduct = Products.Count;

                // Tính tổng số trang dựa trên PageSize và tổng số sản phẩm
                int TotalPage = PageSize > 0 ? (TotalProduct / PageSize) : 0;
                if (TotalProduct % PageSize != 0) TotalPage++;
                ViewData["TotalPage"] = TotalPage;
                ViewData["CurPage"] = PageNum;
                ViewData["txtSearch"] = txtSearch;

                // Lấy danh sách sản phẩm của trang hiện tại
                var pagedProducts = Products.Skip((int)(((PageNum - 1) * PageSize + 1) - 1)).Take(PageSize).ToList();
                Products = pagedProducts;

                return Page();
            }
        }

        public IActionResult OnGetActive(int? id)
        {
            // Nếu không có ID, chuyển hướng đến trang sản phẩm
            if (id == null) return Redirect("~/Manager/Products/index");

            // Tìm sản phẩm theo ID
            BusinessObject.Models.Product product = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (product == null) return Redirect("~/Manager/Products/index");

            // Đảo ngược trạng thái sản phẩm và lưu thay đổi vào cơ sở dữ liệu
            if (product.Status == true)
            {
                product.Status = false;
                product.Discontinued = true;
                //product.QuantityPerUnit = 0;
            }
            else
            {
                product.Status = true;
                product.Discontinued = false;
            }
            _context.SaveChanges();

            return Redirect("~/Manager/Products/index");
        }
    }
}
