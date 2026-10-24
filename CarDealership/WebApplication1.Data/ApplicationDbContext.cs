using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Configuration.Favorites;
using WebApplication1.Data.Configuration.Towns;
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
        public virtual DbSet<Favorite> Favorites { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new TownsConfiguration().Configure(modelBuilder.Entity<Town>());
            new FavoritesConfiguration().Configure(modelBuilder.Entity<Favorite>());
        }
    }
}
