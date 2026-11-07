using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.Data.Repository.Contracts;
using WebApplication1.Services.Core;

namespace WebApplication1.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository favoriteRepository;
        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            this.favoriteRepository = favoriteRepository;
        }
        public async Task<bool> AddToFavoritesAsync(int userId, int carId)
        {
            if (carId <= 0)
            {
                return false;
            }
            if (userId <= 0)
            {
                return false;
            }
            bool doesItemExists = await favoriteRepository.GetAllAsNoTracking().AnyAsync(f => f.CarId == carId && f.UserId == userId);
            try
            {
                if (!doesItemExists)
                {
                    Favorite favCar = new Favorite()
                    {
                        UserId = userId,
                        CarId = carId
                    };
                    favoriteRepository.Add(favCar);
                    await favoriteRepository.SaveChangesAsync();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> RemoveFromFavoritesAsync(int userId, int carId)
        {
            if (carId <= 0)
            {
                return false;
            }
            if (userId <= 0)
            {
                return false;
            }
            try
            {
                Favorite? favorite = await favoriteRepository.GetAllAsNoTracking().Include(f => f.Car).ThenInclude(f => f.Seller).FirstOrDefaultAsync(f => f.UserId == userId && f.CarId == carId);
                if (favorite == null)
                {
                    return false;
                }
                favoriteRepository.Remove(favorite);
                await favoriteRepository.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
