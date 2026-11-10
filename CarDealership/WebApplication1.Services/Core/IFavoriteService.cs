namespace WebApplication1.Services.Core
{
    public interface IFavoriteService
    {
        Task<bool> AddToFavoritesAsync(string userId, Guid carId);
        Task<bool> RemoveFromFavoritesAsync(string userId, Guid carId);
    }
}
