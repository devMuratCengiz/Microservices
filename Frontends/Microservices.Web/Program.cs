using FluentValidation.AspNetCore;
using Microservice.Shared.Services;
using Microservices.Web.Extensions;
using Microservices.Web.Handler;
using Microservices.Web.Helpers;
using Microservices.Web.Models;
using Microservices.Web.Services;
using Microservices.Web.Services.Interfaces;
using Microservices.Web.Validators;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Dynamic;
using System.Security.Principal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));

builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));

builder.Services.AddHttpContextAccessor();

builder.Services.AddAccessTokenManagement();

builder.Services.AddSingleton<PhotoHelper>();

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();


builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddScoped<ClientCredentialTokenHandler>();

builder.Services.AddHttpClientServices(builder.Configuration);


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>
{
    opts.LoginPath = "/Auth/SignIn";
    opts.ExpireTimeSpan = TimeSpan.FromDays(60);
    opts.SlidingExpiration = true;
    opts.Cookie.Name = "microservicewebcookie";
});


// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(fv=>fv.RegisterValidatorsFromAssemblyContaining<CreateCourseInputValidator>());






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
