using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data.Models;
using WebApplication1.Data.Repository.Contracts;
using WebApplication1.Services.Core;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services
{
    public class AdminService : IAdminService
    {
        private readonly ITownRepository townRepository;
        private readonly IUserRepository userRepository;
        private readonly ICarRepository carRepository;
        private readonly IFavoriteRepository favoriteRepository;
        public AdminService(ITownRepository townRepository, IUserRepository userRepository, ICarRepository carRepository, IFavoriteRepository favoriteRepository)
        {
            this.townRepository = townRepository;
            this.userRepository = userRepository;
            this.carRepository = carRepository;
            this.favoriteRepository = favoriteRepository;
        }

        public async Task<bool> CreateTownAsync(string name)
        {
            bool townAlreadyExists = await townRepository.GetAllAsNoTracking().AnyAsync(t => t.Name == name);
            if (townAlreadyExists)
            {
                return false;
            }
            await townRepository.AddAsync(new Town() { Name = name });
            await townRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCarAsync(Guid id)
        {
            Car? car = await carRepository.GetAllAsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (car == null)
            {
                return false;
            }
            IEnumerable<Favorite> favorites = favoriteRepository.GetAllAsNoTracking().Where(f => f.CarId == car.Id);
            if (favorites.Any(f => f.CarId == car.Id))
            {
                favoriteRepository.RemoveRange(favorites);
            }
            carRepository.Remove(car);
            await carRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTownAsync(int id)
        {
            Town? town = await townRepository.GetAllAsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
            if (town == null)
            {
                return false;
            }
            townRepository.Remove(town);
            await townRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            ApplicationUser? user = await userRepository.GetAllAsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return false;
            }
            IEnumerable<Car> userCars = carRepository.GetAllAsNoTracking().Where(c => c.SellerId == user.Id);
            if (userCars.Any())
            {
                carRepository.RemoveRange(userCars);
            }
            userRepository.Remove(user);
            await userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditTownNameAsync(int id, string name)
        {
            if (townRepository.GetAllAsNoTracking().Any(t => t.Name == name))
            {
                return false;
            }
            Town? town = await townRepository.GetAllAsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
            if (town == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            town.Name = name.Trim();
            townRepository.Update(town);
            await townRepository.SaveChangesAsync();
            return true;
        }

        public IQueryable<Car> GetAllCars()
        {
            return carRepository.GetAllAsNoTracking().Include(c => c.Seller).OrderByDescending(c => c.CreatedOn);
        }

        public IQueryable<Town> GetAllTowns()
        {
            return townRepository.GetAllAsNoTracking().Include(t => t.Users).ThenInclude(u => u.Cars).OrderBy(t => t.Name);
        }

        public IQueryable<ApplicationUser> GetAllUsers()
        {
            return userRepository.GetAllAsNoTracking().Include(u => u.Cars).OrderBy(u => u.FirstName).ThenBy(u => u.LastName);
        }

        public async Task<Car?> GetCarByIdAsync(Guid id)
        {
            return await carRepository.GetAllAsNoTracking().Include(c => c.Seller).ThenInclude(s => s.Town).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Town?> GetTownByIdAsync(int id)
        {
            return await townRepository.GetAllAsNoTracking().Include(t => t.Users).ThenInclude(u => u.Cars).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            return await userRepository.GetAllAsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
