using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.Data.Repository;
using WebApplication1.Data.Repository.Contracts;
using WebApplication1.Services.Core;
using WebApplication1.ViewModels.CarViewModels;

namespace WebApplication1.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;
        private readonly IFavoriteRepository favoriteRepository;
        public CarService(ICarRepository carRepository, IFavoriteRepository favoriteRepository)
        {
            this.carRepository = carRepository;
            this.favoriteRepository = favoriteRepository;
        }

        public async Task<bool> CarExistsAsync(Guid id)
        {
            Car? car = await carRepository.GetAll().FirstOrDefaultAsync(c => c.Id == id);
            if (car == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CreateCarAsync(CreateCarViewModel carModel, IFormFile? image, string userId)
        {
            if (userId == null)
            {
                return false;
            }
            Car car = new Car()
            {
                Brand = carModel.Brand,
                Model = carModel.Model,
                Year = carModel.Year,
                Price = carModel.Price,
                Mileage = carModel.Mileage,
                EngineType = carModel.EngineType,
                TransmissionType = carModel.TransmissionType,
                HorsePower = carModel.HorsePower,
                State = carModel.State,
                Description = carModel.Description,
                SellerId = userId,
                CreatedOn = DateTime.Now
            };
            if (image != null)
            {
                using MemoryStream memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);
                car.Image = memoryStream.ToArray();
            }
            carRepository.Add(car);
            await carRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCarAsync(Guid id)
        {
            Car? car = await carRepository.GetAllAsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (car == null)
            {
                return false;
            }
            IEnumerable<Favorite> favorites = await favoriteRepository.GetAll().Where(f => f.CarId == id).ToListAsync();
            favoriteRepository.RemoveRange(favorites);
            carRepository.Remove(car);
            await carRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditCarAsync(EditCarViewModel carModel, IFormFile? image, string userId, Guid carId)
        {
            //validating the car
            if (carId != carModel.Id)
            {
                return false;
            }
            Car? car = carRepository.GetAll().Include(c => c.Seller).FirstOrDefault(c => c.Id == carId);
            //checking if the car exists
            if (car == null)
            {
                return false;
            }
            if (car.SellerId != userId)
            {
                return false;
            }
            if (car.Seller == null)
            {
                return false;
            }
            //setting the car new properties
            if (image != null)
            {
                using MemoryStream memoryStream = new MemoryStream();
                image.CopyTo(memoryStream);
                car.Image = memoryStream.ToArray();
            }
            car.Brand = carModel.Brand;
            car.Model = carModel.Model;
            car.Year = carModel.Year;
            car.Price = carModel.Price;
            car.Mileage = carModel.Mileage;
            car.EngineType = carModel.EngineType;
            car.TransmissionType = carModel.TransmissionType;
            car.HorsePower = carModel.HorsePower;
            car.State = carModel.State;
            car.Description = carModel.Description;
            //saving the changes in the context
            await carRepository.SaveChangesAsync();
            return true;
        }

        public async Task<Car?> GetDetailsAsync(Guid id)
        {
            Car? car = await carRepository.GetAllAsNoTracking().Include(c => c.Seller).ThenInclude(s => s.Town).FirstOrDefaultAsync(c => c.Id == id);
            if (car == null)
            {
                return null;
            }
            return car;
        }

        public IEnumerable<Car> ShowAllCars()
        {
            IEnumerable<Car> cars = carRepository.GetAll().OrderByDescending(c => c.CreatedOn).ToArray();
            return cars;
        }

        public IEnumerable<Car> ShowSearchResults(string searchText)
        {
            IEnumerable<Car> carsFound = carRepository.GetAll().Include(c => c.Seller).Where(c => c.Brand.ToLower().Contains(searchText.ToLower())).ToArray();
            return carsFound;
        }
    }
}
