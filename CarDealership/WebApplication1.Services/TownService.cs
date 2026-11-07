using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.Data.Repository.Contracts;
using WebApplication1.Services.Core;
using WebApplication1.ViewModels;

namespace WebApplication1.Services
{
    public class TownService : ITownService
    {
        private readonly ITownRepository townRepository;
        private readonly ICarRepository carRepository;
        public TownService(ITownRepository townRepository, ICarRepository carRepository)
        {
            this.townRepository = townRepository;
            this.carRepository = carRepository;
        }

        public IQueryable<Car> GetAllCarsFromTown(int id)
        {
            IQueryable<Car> cars = carRepository.GetAllAsNoTracking().Where(c => c.Seller.TownId == id).AsNoTracking();
            return cars;
        }

        public async Task<Town?> GetTownDetailsAsync(int id)
        {
            Town? town = await townRepository.GetAllAsNoTracking().Include(t => t.Users).ThenInclude(u => u.Cars).FirstOrDefaultAsync(t => t.Id == id);
            return town;
        }

        public async Task<IEnumerable<Town>> GetTownsAsync()
        {
            IEnumerable<Town> towns = await townRepository.GetAll().Include(t => t.Users).ThenInclude(u => u.Cars).OrderBy(t => t.Name).ToArrayAsync();
            return towns;
        }
    }
}
