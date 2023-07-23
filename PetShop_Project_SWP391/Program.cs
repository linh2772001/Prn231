using BusinessObject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ProjectContext>();
builder.Services.AddSession(opt => opt.IdleTimeout = TimeSpan.FromMinutes(5));
builder.Services.AddSignalR();
//cookie authen
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/account/singnin"; 
        options.AccessDeniedPath = "/error";
    });

var app = builder.Build();
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
//app.UseCookiePolicy();
app.MapRazorPages();
app.Run();