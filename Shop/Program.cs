using System;
using Microsoft.EntityFrameworkCore;
using Shop;
using Shop.Interfaces;
using Shop.Models;
using Shop.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<AppDBContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IAdminAllProducts, AdminRepository>();
builder.Services.AddTransient<IAllProducts, ProductRepository>();
builder.Services.AddTransient<IAllShopCart, ShopCartRepository>();
builder.Services.AddTransient<IAllOrders, OrderRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ShopCart>(s => ShopCartRepository.GetCart(s));

builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddMvc();
builder.Services.AddMemoryCache();

builder.Configuration.AddJsonFile("dbsettings.json");
//builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Добавляем контекст базы данных с использованием строки подключения
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseStatusCodePages();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/Index",
    defaults: new { Controller = "Admin", action = "Index" });

app.Run();
