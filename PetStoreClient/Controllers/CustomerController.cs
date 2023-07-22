using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace PetStoreClient.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient client = null;
        private string DefaultApiUrl = "";
        private string DefaultApiUrlCusomer = "";

        public CustomerController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            DefaultApiUrl = "https://localhost:7001/api/Account";
            DefaultApiUrlCusomer = "https://localhost:7001/api/Customer";
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("PetSession") == null)
                return RedirectToAction("Index", "Login");

            ClaimsPrincipal claimsPrincipal = HttpContext.User as ClaimsPrincipal;
            string email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

            HttpResponseMessage response = await client.GetAsync(DefaultApiUrl + "/Infomation?email=" + email);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                var jsonDocument = JsonDocument.Parse(responseContent);

                string customerId = jsonDocument.RootElement.GetProperty("customerId").GetString();

                string customerUrl = DefaultApiUrlCusomer + "/id?id=" + customerId;
                HttpResponseMessage customerResponse = await client.GetAsync(customerUrl);

                if (customerResponse.IsSuccessStatusCode)
                {
                    string customerContent = await customerResponse.Content.ReadAsStringAsync();
                    CustomerDTO customerInfo = JsonSerializer.Deserialize<CustomerDTO>(customerContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return View(customerInfo);
                }
            }
            return View();
        }
    }
}
