using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
/*using PetShop_Project_SWP391.Models;*/
using System.Text.Json;

namespace PetShop_Project_SWP391.Pages.Customers
{
    [Authorize(Roles = "2")]
    public class CustomersOrderDetailsModel : PageModel
    {
        private readonly ProjectContext dBContext;
        public CustomersOrderDetailsModel(ProjectContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [BindProperty]
        public @BusinessObject.Models.Account Account { get; set; }
        [BindProperty]
        public BusinessObject.Models.Shipper Shipper { get; private set; }

        [BindProperty]
        public Customer Customer { get; set; }

        [BindProperty]
        public @BusinessObject.Models.Order? Order { get; set; }

        [BindProperty]
        public OrderDetail OrderDetails { get; set; }

        [BindProperty]
        public Ship Ship { get; set; }

        [BindProperty]
        public float SumAllPrice { get; set; }

        [BindProperty]
        public float SumPriceSubtotal { get; set; }

        public List<BusinessObject.Models.Product> Products { get; set; }
        public async Task<IActionResult> OnGetAsync(int OrderId)
        {
            if (HttpContext.Session.GetString("PetSession") == null)
                return RedirectToPage("/account/singnin");

            Account = JsonSerializer.Deserialize<@BusinessObject.Models.Account>(HttpContext.Session.GetString("PetSession"));
            Customer = dBContext.Customers.FirstOrDefault(c => c.CustomerId == Account.CustomerId);

            if (OrderId == null || dBContext.OrderDetails == null)
            {
                return NotFound();
            }

            var order = await dBContext.Orders.Include(p => p.OrderDetails).FirstOrDefaultAsync(o => o.OrderId == OrderId);
            var orderDetail = await dBContext.OrderDetails.FirstOrDefaultAsync(od => od.OrderId == OrderId);

            var shipDetail = await dBContext.Ships.FirstOrDefaultAsync(s => s.OrderId == OrderId);

            Products = dBContext.Products.ToList();

            if (order == null)
            {
                return NotFound();
            }
            else
            {
                Ship = shipDetail;
                Order = order;
                OrderDetails = orderDetail;
                SumAllPrice = (float)orderDetail.UnitPrice * orderDetail.Quantity + (float)shipDetail.Freight;
                SumPriceSubtotal = (float)orderDetail.UnitPrice * orderDetail.Quantity;
            }

            return Page();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostActiveAsync(int OrderId)
        {
            BusinessObject.Models.Order order = dBContext.Orders.FirstOrDefault(o => o.OrderId == OrderId);
            Order = order;

            if (Order.OrderStatus == "Future")
            {
                Order.OrderStatus = "Cancel";
                await dBContext.SaveChangesAsync(); // Save the changes to the database
            }

            return RedirectToPage("/Customers/CustomerInfomation"); // Redirect to the same page after updating the order status
        }

    }
}