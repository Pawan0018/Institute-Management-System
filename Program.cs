using InstitudeManagement.Models;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // Configure DbContext with SQL Server using the connection string from appsettings.json
        builder.Services.AddDbContext<TeknowellContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));

        // Add session services
        builder.Services.AddSession();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts(); // Enforce HTTPS with HSTS
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        // Enable session middleware
        app.UseSession();

        app.UseRouting();

        app.UseAuthorization();

        // Define default routing
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Login}/{id?}");

        app.Run();
    }
}
