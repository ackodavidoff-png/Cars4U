using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.Services.Core;
using WebApplication1.ViewModels;

namespace WebApplication1.Services
{
    public class TownService : ITownService
    {
        private readonly ApplicationDbContext dbContext;
        public TownService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Car> GetAllCarsFromTown(int id)
        {
            IQueryable<Car> cars = dbContext.Cars.Where(c => c.Seller.TownId == id).AsNoTracking();
            return cars;
        }

        public async Task<Town?> GetTownDetailsAsync(int id)
        {
            Town? town = await dbContext.Towns.Include(t => t.Users).ThenInclude(u => u.Cars).FirstOrDefaultAsync(t => t.Id == id);
            return town;
        }

        public async Task<IEnumerable<Town>> GetTownsAsync()
        {
            IEnumerable<Town> towns = await dbContext.Towns.Include(t => t.Users).ThenInclude(u => u.Cars).OrderBy(t => t.Name).ToArrayAsync();
            return towns;
        }
    }
}
