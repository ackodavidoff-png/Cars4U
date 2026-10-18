using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data.Models;

namespace WebApplication1.Data.Configuration.Towns
{
    public class TownsConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasData(new Town[]
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
