using Microsoft.AspNetCore.Mvc;

namespace PetStoreClient.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
