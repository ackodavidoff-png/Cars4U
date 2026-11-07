using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.Data.Repository;
using WebApplication1.Data.Repository.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.Core;
using static WebApplication1.GCommon.IdentityConstants;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            //Console.WriteLine($"SQL Server instance: {new Microsoft.Data.SqlClient.SqlConnectionStringBuilder(connectionString).DataSource}");
            //Environment.Exit(0);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = RequireConfirmedAccount;
                options.Password.RequireDigit = RequireDigit;
                options.Password.RequireNonAlphanumeric = RequireNonAlphanumeric;
                options.Password.RequireUppercase = RequireUppercase;
                options.Password.RequireLowercase = RequireLowercase;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();

            //services
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IFavoriteService, FavoriteService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITownService, TownService>();

            //repositories
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            builder.Services.AddScoped<ITownRepository, TownRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
