using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using capanna.alessandro._5H.prenota.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<dataBase>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbContext") ?? throw new InvalidOperationException("Connection string 'dbContext' not found.")));

builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddIdentity<Utente, IdentityRole>(
    options => {
        options.Password.RequiredUniqueChars = 1;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength= 8;
        options.Password.RequireNonAlphanumeric = true;
    }
).AddEntityFrameworkStores<dataBase>().AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
