using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Services.Core
{
    public interface ITownService
    {
        Task<IEnumerable<Town>> GetTownsAsync();
        Task<Town?> GetTownDetailsAsync(int id);
        IQueryable<Car> GetAllCarsFromTown(int id);
    }
}
