using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data.Models;

namespace WebApplication1.Data.Configuration.Favorites
{
    public class FavoritesConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.HasKey(f => new
            {
                f.UserId,
                f.CarId
            });
            builder
                .HasOne(f => f.ApplicationUser)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(f => f.Car)
                .WithMany()
                .HasForeignKey(f => f.CarId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
