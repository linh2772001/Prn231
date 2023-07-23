using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PetStoreClient.Controllers
{
    public class ProductController : Controller
    {
       // [Authorize(Roles ="1")]
        public async Task<IActionResult> Index(int id)
        {
            return View();
        }
    }
}
