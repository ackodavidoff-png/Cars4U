namespace WebApplication1.Services.Core
{
    public interface IFavoriteService
    {
        Task<bool> AddToFavoritesAsync(int userId, int carId);
        Task<bool> RemoveFromFavoritesAsync(int userId, int carId);
    }
}
