﻿using BusinessObject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
/*using PetShop_Project_SWP391.Models;*/
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetShop_Project_SWP391.Pages.Account
{
    public class SingnInModel : PageModel
    {
        private readonly ProjectContext projectContext;

        public SingnInModel(ProjectContext projectContext)
        {
            this.projectContext = projectContext;
        }

        [BindProperty]
        public BusinessObject.Models.Account Account { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(Account.Email) || string.IsNullOrEmpty(Account.Password))
                {
                    ViewData["msgEmpty"] = "Vui lòng nhập cả email và mật khẩu";
                    return Page();
                }

                var acc = await projectContext.Accounts.SingleOrDefaultAsync(a => a.Email.Equals(Account.Email) && a.Password.Equals(Account.Password));

                if (acc != null)
                {
                    if (acc.IsActive == true)
                    {
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, acc.Email),
                        new Claim(ClaimTypes.Role, acc.Role.ToString())
                    };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = false,
                            AllowRefresh = true,
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                        HttpContext.Session.SetString("PetSession", JsonSerializer.Serialize(acc));

                        if (acc.Role == 2)
                        {
                            return RedirectToPage("/index");
                        }
                        else if (acc.Role == 1)
                        {
                            return RedirectToPage("/manager/index");
                        }
                        else if (acc.Role == 3)
                        {
                            return RedirectToPage("/shipper/index");
                        }
                        return Page();
                    }
                    else
                    {
                        ViewData["msgActiveAccount"] = "Tài khoản hiện tại đang không hoạt động";
                        return Page();
                    }
                }
                else
                {
                    ViewData["msgEmailPass"] = "Tài khoản hoặc mật khẩu không chính xác";
                    return Page();
                }
            }
            else
            {
                return Page();
            }
        }
    }
}