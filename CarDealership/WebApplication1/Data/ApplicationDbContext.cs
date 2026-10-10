using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Models;
using WebApplication1.Data.Models.Enums;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        //public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<Town> Towns { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Cars4U;Trusted_Connection=True;Encrypt=false");  
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser()
            //{
            //    Id = 1,
            //    FirstName = "Ivan",
            //    LastName = "Ivanov",
            //    Username = "Vankata04",
            //    PhoneNumber = "0888555123",
            //    IsAdmin = true,
            //    TownId = 1
            //},
            //new ApplicationUser()
            //{
            //    Id = 2,
            //    FirstName = "Georgi",
            //    LastName = "Petrov",
            //    Username = "Go6o.petroff",
            //    Email = "go6opetroff@something.com",
            //    PhoneNumber = "0899123456",
            //    IsAdmin = false,
            //    TownId= 2
            //},
            //new ApplicationUser()
            //{
            //    Id = 3,
            //    FirstName = "Stojan",
            //    LastName = "Dimitrov",
            //    Username = "stojandmtrv",
            //    Email = "st_dimitrov@mail.com",
            //    PhoneNumber = "0887654321",
            //    TownId = 3
            //});
            //modelBuilder.Entity<Car>().HasData(new Car()
            //{
            //    Id = 1,
            //    Brand = "Peugeot",
            //    Model = "308SW",
            //    Year = 2015,
            //    Price = 10000,
            //    Mileage = 150000,
            //    EngineType = EngineType.Diesel,
            //    TransmissionType = TransmissionType.Automatic,
            //    HorsePower = 250,
            //    State = State.Used,
            //    Description = "Real kilometres,very well preserved.This car has never been in an accident.",
            //    CreatedOn = new DateTime(2026, 9, 21),
            //    SellerId = 1
            //},
            //new Car()
            //{
            //    Id = 2,
            //    Brand = "Toyota",
            //    Model = "Yaris",
            //    Year = 2018,
            //    Price = 4000,
            //    Mileage = 80000,
            //    EngineType = EngineType.Hybrid,
            //    TransmissionType = TransmissionType.Automatic,
            //    HorsePower = 180,
            //    State = State.Used,
            //    Description = "Very well preserved.",
            //    CreatedOn = new DateTime(2026, 9, 21),
            //    SellerId = 2
            //},
            //new Car()
            //{
            //    Id = 3,
            //    Brand = "VW",
            //    Model = "Golf",
            //    Year = 2020,
            //    Price = 7000,
            //    Mileage = 50000,
            //    EngineType = EngineType.Gasoline,
            //    TransmissionType = TransmissionType.Manual,
            //    HorsePower = 250,
            //    State = State.Used,
            //    Description = "Very well preserved.The car is good for in-town and out-of-town driving.",
            //    CreatedOn = new DateTime(2026, 9, 21),
            //    SellerId = 3
            //});
            modelBuilder.Entity<Town>().HasData(new Town[]
            {
                new Town() { Id = 1, Name = "Sofia"},
                new Town() { Id = 2, Name = "Plovdiv"},
                new Town() { Id = 3, Name = "Varna"},
                new Town() { Id = 4, Name = "Burgas"},
                new Town() { Id = 5, Name = "Ruse"},
                new Town() { Id = 6, Name = "Stara Zagora"},
                new Town() { Id = 7, Name = "Pleven"},
                new Town() { Id = 8, Name = "Sliven"},
                new Town() { Id = 9, Name = "Dobrich"},
                new Town() { Id = 10, Name = "Shumen"},
                new Town() { Id = 11, Name = "Pernik"},
                new Town() { Id = 12, Name = "Haskovo"},
                new Town() { Id = 13, Name = "Yambol"},
                new Town() { Id = 14, Name = "Pazardzhik"},
                new Town() { Id = 15, Name = "Blagoevgrad"},
                new Town() { Id = 16, Name = "Veliko Tarnovo"},
                new Town() { Id = 17, Name = "Vratsa"},
                new Town() { Id = 18, Name = "Gabrovo"},
                new Town() { Id = 19, Name = "Vidin"},
                new Town() { Id = 20, Name = "Montana"},
                new Town() { Id = 21, Name = "Kyustendil"},
                new Town() { Id = 22, Name = "Kardzhali"},
                new Town() { Id = 23, Name = "Targovishte"},
                new Town() { Id = 24, Name = "Lovech"},
                new Town() { Id = 25, Name = "Silistra"},
                new Town() { Id = 26, Name = "Razgrad"},
                new Town() { Id = 27 ,Name = "Smolyan"}
            });
        }
    }
}
