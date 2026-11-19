using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.Data.Repository;
using WebApplication1.Data.Repository.Contracts;
using WebApplication1.Data.Seeding.Utilities;
using WebApplication1.Services;
using WebApplication1.Services.Core;
using WebApplication1.Services.Data.AdminUser.Request;
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

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = RequireConfirmedAccount;
                options.Password.RequireDigit = RequireDigit;
                options.Password.RequireNonAlphanumeric = RequireNonAlphanumeric;
                options.Password.RequireUppercase = RequireUppercase;
                options.Password.RequireLowercase = RequireLowercase;
            }).AddRoles<IdentityRole>()
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddDefaultTokenProviders()
            .AddDefaultUI()
            .AddEntityFrameworkStores<ApplicationDbContext>();

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

            UseDefaultRoles(app, app.Configuration);

            app.Run();
        }
        private static void UseDefaultRoles(WebApplication app, IConfiguration configuration)
        {
            using IServiceScope serviceScope = app.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            string adminFirstName = configuration.GetValue<string>("AdminUser:FirstName") ?? throw new ArgumentException();
            string adminLastName = configuration.GetValue<string>("AdminUser:LastName") ?? throw new ArgumentException();
            string adminUsername = configuration.GetValue<string>("AdminUser:UserName") ?? throw new ArgumentException();
            string adminEmail = configuration.GetValue<string>("AdminUser:Email") ?? throw new ArgumentException();
            int adminTownId = configuration.GetValue<int>("AdminUser:TownId");
            string adminPhoneNumber = configuration.GetValue<string>("AdminUser:PhoneNumber") ?? throw new ArgumentException();
            string adminPassword = configuration.GetValue<string>("AdminUser:Password") ?? throw new ArgumentException();
            IdentitySeeder.SeedRoles(serviceProvider);
            AdminUserSeedRequestModel requestModel = new AdminUserSeedRequestModel()
            {
                FirstName = adminFirstName,
                LastName = adminLastName,
                UserName = adminUsername,
                Email = adminEmail,
                TownId = adminTownId,
                PhoneNumber = adminPhoneNumber,
                Password = adminPassword
            };
            IdentitySeeder.SeedAdminUser(serviceProvider, requestModel);
        }
    }
}
