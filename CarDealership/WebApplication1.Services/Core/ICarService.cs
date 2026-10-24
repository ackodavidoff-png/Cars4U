using Microsoft.AspNetCore.Http;
using WebApplication1.Data.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Services.Core
{
    public interface ICarService
    {
        IEnumerable<Car> ShowAllCars();
        Task<bool> CarExistsAsync(int id);
        Task<Car?> GetDetailsAsync(int id);
        Task<bool> DeleteCarAsync(int id);
        Task<bool> CreateCarAsync(CreateCarViewModel carModel, IFormFile? image, int userId);
        Task<bool> EditCarAsync(EditCarViewModel carModel, IFormFile? image, int userId, int carId);
        IEnumerable<Car> ShowSearchResults(string searchText);
    }
}
