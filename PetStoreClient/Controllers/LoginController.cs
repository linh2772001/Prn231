using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace PetStoreClient.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient client = null;
        private string DefaultApiUrl = "";
        public LoginController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            DefaultApiUrl = "https://localhost:7001/api/Account";
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm, Bind("Email", "Password")] LoginDTO loginInfo,
                                               [FromForm, Bind("ReturnUrl")] string returnUrl)
        {
            try
            {
                var stringContent = new StringContent(JsonSerializer.Serialize<LoginDTO>(loginInfo), Encoding.UTF8, "application/json");
                HttpResponseMessage roleResponse = await client.GetAsync(DefaultApiUrl + "/role?email=" + loginInfo.Email);
                HttpResponseMessage response = await client.PostAsync(DefaultApiUrl + "/login", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    int role = await roleResponse.Content.ReadAsAsync<int>();

                    // Create claims for email and role
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, loginInfo.Email),
                new Claim(ClaimTypes.Role, role.ToString())
            };

                    // Create claims identity
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Sign in the user
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    HttpContext.Session.SetString("PetSession", loginInfo.Email);
                    // Redirect based on role
                    if (role == 1)
                    {
                        return Redirect("/Manager/index");
                    }
                    else if (role == 2)
                    {
                        return Redirect("/Home/index");
                    }
                    return View(loginInfo);
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                    return View(loginInfo);
                }
            }
            catch (Exception ex)
            {
                ViewData["Login"] = ex.Message;
                return View();
            }
        }

    }
}
