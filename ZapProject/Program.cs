using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZapProject.Data;
using ZapProject.Data.Interfaces;
using ZapProject.Data.Repository;
using ZapProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFoodItems, FoodItemsRepository>();
builder.Services.AddScoped<IFavItems, FavouriteItemsRepository>();
builder.Services.AddScoped<IDashboardService, DashboardRepository>();
builder.Services.AddScoped<IOrdersService, OrdersRepository>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

var app = builder.Build();


//if (args.Length == 1 && args[0].ToLower() == "seeddata")
//{
//    await Seed.SeedUsersAndRolesAsync(app);
//}


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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

//app.MapControllerRoute(
//    name: "categoryFilter",
//    pattern: "{controller}/{category?}",
//    defaults: new { controller = "FoodItems", action = "Index" });

app.Run();
