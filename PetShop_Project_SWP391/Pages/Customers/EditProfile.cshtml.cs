using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
/*using PetShop_Project_SWP391.Models;*/
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.RegularExpressions;
namespace PetShop_Project_SWP391.Pages.Customers
{
    [Authorize(Roles = "2")]
    public class EditProfileModel : PageModel
    {
        private readonly ProjectContext projectContext;

        public EditProfileModel(ProjectContext dBContext, IConfiguration configuration)
        {
            this.projectContext = dBContext;
        }

        [BindProperty]
        public @BusinessObject.Models.Account Account { get; set; }
        [BindProperty]
        public Customer Customer { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Session.GetString("PetSession") == null)
                return RedirectToPage("/account/singnin");

            Account = JsonSerializer.Deserialize<BusinessObject.Models.Account>(HttpContext.Session.GetString("PetSession"));
            Customer = projectContext.Customers.FirstOrDefault(c => c.CustomerId == Account.CustomerId);
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            Account = JsonSerializer.Deserialize<BusinessObject.Models.Account>(HttpContext.Session.GetString("PetSession"));
            if (
               string.IsNullOrEmpty(Customer.Name) ||
               string.IsNullOrEmpty(Customer.Address) ||
               string.IsNullOrEmpty(Customer.District) ||
               string.IsNullOrEmpty(Customer.Province) ||
               string.IsNullOrEmpty(Customer.Wards) ||
               string.IsNullOrEmpty(Customer.Phone))
            {
                ViewData["msgEmpty"] = "Vui lòng nhập cả tất cả thông tin bên trên";
                return Page();
            }
            if (!Regex.IsMatch(Customer.Phone, @"^0\d{9}$"))
            {
                ViewData["msgPhone"] = "Số điện thoại không hợp lệ. Vui lòng nhập số bắt đầu từ 0 và có 10 chữ số!";
                return Page();
            }
            var cus = await projectContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == Account.CustomerId);
            if (cus != null)
            {
                cus.Name = Customer.Name;
                cus.Address = Customer.Address;
                cus.District = Customer.District;
                cus.Province = Customer.Province;
                cus.Wards = Customer.Wards;
                cus.Phone = Customer.Phone;
                cus.CreateDate = DateTime.Now;
                await projectContext.SaveChangesAsync();
                ViewData["msgSuccess"] = "Thay đổi thông tin thành công";
            };
            return RedirectToPage("/Customers/Profile");
        }
    }
}
