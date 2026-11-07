using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data.Models;
using WebApplication1.Data.Repository.Common;
using WebApplication1.Data.Repository.Contracts;

namespace WebApplication1.Data.Repository
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Car?> FindByIdAsync(int id)
        {
            return await base.DbSet.FindAsync(id);
        }
    }
}
