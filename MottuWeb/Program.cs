using Microsoft.AspNetCore.Authentication.Cookies;
using MottuWeb.Service;
using MottuWeb.Service.IService;
using MottuWeb.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IServiceMotorcycle, ServiceMotorcycle>();
builder.Services.AddHttpClient<IServiceAuth, ServiceAuth>();
builder.Services.AddHttpClient<IServiceLocation, ServiceLocation>();


Configs.MotorcycleAPIBase = builder.Configuration["ServiceUrls:MotorcycleAPI"];
Configs.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];
Configs.LocationAPIBase = builder.Configuration["ServiceUrls:LocationAPI"];
Configs.OrderAPIBase = builder.Configuration["ServiceUrls:OrderAPI"];

builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IServiceBase, ServiceBase>();
builder.Services.AddScoped<IServiceMotorcycle, ServiceMotorcycle>();
builder.Services.AddScoped<IServiceLocation, ServiceLocation>();
builder.Services.AddScoped<IServiceOrder, ServiceOrder>();
builder.Services.AddScoped<IServiceAuth, ServiceAuth>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => 
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });


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
