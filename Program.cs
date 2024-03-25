using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using lab1.Data; // Ensure this matches the actual location of your Lab1Context.
using lab1.Models; // Include this only if you have models specifically referenced outside of Lab1Context.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Lab1Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); // UseSqlite is specified here for SQLite database.

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<Lab1Context>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Authentication middleware is called before authorization middleware.
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
