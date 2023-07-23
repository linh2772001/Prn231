using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
/*using PetShop_Project_SWP391.Models;*/
using System.Linq;
using System.Threading.Tasks;

namespace PetShop_Project_SWP391.Pages.Manager.TableOrderList
{
    public class DetailModel : PageModel
    {
        private readonly ProjectContext _projectContext;

        public DetailModel(ProjectContext projectContext)
        {
            _projectContext = projectContext;
        }

        [BindProperty]
        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int orderId)
        {
            Order = await _projectContext.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (Order == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostActiveAsync(int orderId)
        {
            var order = await _projectContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            // Update the order status to "Delivered" or any other status you prefer
            order.OrderStatus = "Complete";

            // Save changes to the database
            await _projectContext.SaveChangesAsync();

            // Redirect back to the Order Detail page
            return RedirectToPage("Detail", new { orderId = orderId });
        }
    }
}
