using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;
using System.Text.Json;

namespace PetShop_Project_SWP391.Pages.Product
{
    [Authorize(Roles = "2")]
    public class CartModel : PageModel
    {
        private readonly ProjectContext db;
        public CartModel(ProjectContext db)
        {
            this.db = db;
        }
        [BindProperty]
        public Ship Ship { get; set; }
        [BindProperty]
        public Customer Customer { get; set; }
        [BindProperty]
        public BusinessObject.Models.Account Account { get; set; }

        [BindProperty]
        public List<Cart>? CartItems { get; set; }

        [BindProperty]
        decimal? total { get; set; }
        public IActionResult OnGet()
        {
            string? cart = HttpContext.Session.GetString("CartSession");

            if (cart == null)
            {
                ViewData["emptymess"] = "Cart empty!";
                return Page();
            }

            CartItems = JsonSerializer.Deserialize<List<Cart>>(cart);
            total = GetTotal(CartItems);

            if (HttpContext.Session.GetString("PetSession") != null)
            {
                Account = JsonSerializer.Deserialize<@BusinessObject.Models.Account>(HttpContext.Session.GetString("PetSession"));
                Customer = db.Customers.FirstOrDefault(c => c.CustomerId == Account.CustomerId);
            }

            ViewData["total"] = total;
            return Page();
        }
        public IActionResult OnGetBuyNow(int id)
        {
            string? cart = HttpContext.Session.GetString("CartSession");
            BusinessObject.Models.Product product = db.Products.FirstOrDefault(p => p.ProductId == id);
            total = 0;

            if (cart == null)
            {
                //tao mot gio hang moi va them san pham do vao
                CartItems = new List<Cart>();
                CartItems.Add(new Cart(product, 1));
                total += product.UnitPrice;
            }
            else
            {
                string? cartsdeserialize = HttpContext.Session.GetString("CartSession");
                //deserialize Dictionary from string got from session
                CartItems = JsonSerializer.Deserialize<List<Cart>>(cartsdeserialize);
                total = GetTotal(CartItems);

                int index = getIndexOfProductInCart(product, CartItems);
                if (index != -1)
                    CartItems[index].Quantity++; //tang so luong
                else
                    CartItems.Add(new Cart(product, 1)); //them san pham 

                total += product.UnitPrice;
            }

            //serialize dictionary to string to store in session
            string cartsserialize = JsonSerializer.Serialize(CartItems);
            HttpContext.Session.SetString("CartSession", cartsserialize);

            string totalserialize = JsonSerializer.Serialize(total);
            HttpContext.Session.SetString("total", totalserialize);

            if (HttpContext.Session.GetString("PetSession") != null)
            {
                Account = JsonSerializer.Deserialize<@BusinessObject.Models.Account>(HttpContext.Session.GetString("PetSession"));
                Customer = db.Customers.FirstOrDefault(c => c.CustomerId == Account.CustomerId);
            }

            ViewData["total"] = total;
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (Customer.Name != null && Customer.Phone != null
                && Customer.Province != null && Customer.Address != null )
            {
                var session = HttpContext.Session.GetString("PetSession");
                var acc = new BusinessObject.Models.Account();
                var cus = new Customer();
                if (session is not null)
                {
                    Account = JsonSerializer.Deserialize<@BusinessObject.Models.Account>(HttpContext.Session.GetString("PetSession"));
                    Customer = db.Customers.FirstOrDefault(c => c.CustomerId == Account.CustomerId);
                }
                else
                {
                    return RedirectToPage("/account/singnin");
                }

                var o = new BusinessObject.Models.Order()
                {
                    CustomerId = Customer.CustomerId,
                    OrderDate = DateTime.Now,
                    ShippedDate = DateTime.Now.AddDays(7),
                    OrderStatus = "Future",
                };

                await db.Orders.AddAsync(o);
                await db.SaveChangesAsync();

                int LastID = db.Orders.OrderBy(x => x.OrderId).Select(x => x.OrderId).Last();

                string? cartsdeserialize = HttpContext.Session.GetString("CartSession");
                CartItems = JsonSerializer.Deserialize<List<Cart>>(cartsdeserialize);

                foreach (var item in CartItems)
                {
                    var od = new OrderDetail()
                    {
                        OrderId = LastID,
                        ProductId = item.Product.ProductId,
                        UnitPrice = (decimal)item.Product.UnitPrice,
                        Quantity = (short)item.Quantity,
                        Discount = 0
                    };
                    
                    await db.OrderDetails.AddAsync(od);
                    await db.SaveChangesAsync();
                }
                HttpContext.Session.Remove("CartSession");

                if (session is null) return RedirectToPage("/index");
                ViewData["msg"] = "Oreder sucessful";
                return RedirectToPage("/Customers/CustomerInfomation");

            }
            else
            {
                return RedirectToPage("/product/cart");
            }
        }

        public IActionResult OnGetDelete(int id)
        {
            string? cartsdeserialize = HttpContext.Session.GetString("CartSession");
            CartItems = JsonSerializer.Deserialize<List<Cart>>(cartsdeserialize);
            BusinessObject.Models.Product product = db.Products.FirstOrDefault(p => p.ProductId == id);
            int index = getIndexOfProductInCart(product, CartItems);
            CartItems.RemoveAt(index);
            total = GetTotal(CartItems);

            //serialize dictionary to string to store in session
            string cartsserialize = JsonSerializer.Serialize(CartItems);
            HttpContext.Session.SetString("CartSession", cartsserialize);

            string totalserialize = JsonSerializer.Serialize(total);
            HttpContext.Session.SetString("total", totalserialize);

            if (CartItems.Count == 0)
            {
                ViewData["emptymess"] = "Cart empty!";
            }
            ViewData["total"] = total;
            return RedirectToPage("/product/cart");
        }

        public IActionResult OnGetPlus(int id)
        {
            string? cartsdeserialize = HttpContext.Session.GetString("CartSession");
            CartItems = JsonSerializer.Deserialize<List<Cart>>(cartsdeserialize);
            BusinessObject.Models.Product product = db.Products.FirstOrDefault(p => p.ProductId == id);
            int index = getIndexOfProductInCart(product, CartItems);
            CartItems[index].Quantity++;
            total = GetTotal(CartItems);

            //serialize dictionary to string to store in session
            string cartsserialize = JsonSerializer.Serialize(CartItems);
            HttpContext.Session.SetString("CartSession", cartsserialize);

            string totalserialize = JsonSerializer.Serialize(total);
            HttpContext.Session.SetString("total", totalserialize);

            if (CartItems.Count == 0)
            {
                ViewData["emptymess"] = "Cart empty!";
            }
            ViewData["total"] = total;
            return RedirectToPage("/product/cart");
        }

        public IActionResult OnGetMinus(int id)
        {
            string? cartsdeserialize = HttpContext.Session.GetString("CartSession");
            CartItems = JsonSerializer.Deserialize<List<Cart>>(cartsdeserialize);
            BusinessObject.Models.Product product = db.Products.FirstOrDefault(p => p.ProductId == id);
            int index = getIndexOfProductInCart(product, CartItems);
            CartItems[index].Quantity--;

            if (CartItems[index].Quantity == 0) CartItems.Remove(CartItems[index]);

            total = GetTotal(CartItems);

            //serialize dictionary to string to store in session
            string cartsserialize = JsonSerializer.Serialize(CartItems);
            HttpContext.Session.SetString("CartSession", cartsserialize);

            string totalserialize = JsonSerializer.Serialize(total);
            HttpContext.Session.SetString("total", totalserialize);

            if (CartItems.Count == 0)
            {
                ViewData["emptymess"] = "Cart empty!";
            }
            ViewData["total"] = total;
            return RedirectToPage("/product/cart");
        }

        public decimal? GetTotal(List<Cart> carts)
        {
            decimal? total = 0;
            foreach (Cart cart in carts)
            {
                total += cart.Product.UnitPrice * cart.Quantity;
            }
            return total;

        }

        public int getIndexOfProductInCart(BusinessObject.Models.Product product, List<Cart> carts)
        {

            for (int i = 0; i < carts.Count; i++)
            {
                if (carts[i].Product.ProductId == product.ProductId) return i;
            }
            return -1;
        }

        private string GenerateCusID(int length)
        {
            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            return str_build.ToString();
        }
    }
}
