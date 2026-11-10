using Microsoft.AspNetCore.Http;
using WebApplication1.Data.Models;
using WebApplication1.ViewModels.CarViewModels;

namespace WebApplication1.Services.Core
{
    public interface ICarService
    {
        IEnumerable<Car> ShowAllCars();
        Task<bool> CarExistsAsync(Guid id);
        Task<Car?> GetDetailsAsync(Guid id);
        Task<bool> DeleteCarAsync(Guid id);
        Task<bool> CreateCarAsync(CreateCarViewModel carModel, IFormFile? image, string userId);
        Task<bool> EditCarAsync(EditCarViewModel carModel, IFormFile? image, string userId, Guid carId);
        IEnumerable<Car> ShowSearchResults(string searchText);
    }
}
