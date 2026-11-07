using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data.Models;
using WebApplication1.Data.Repository.Contracts.Common;

namespace WebApplication1.Data.Repository.Contracts
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<Car?> FindByIdAsync(int id);
    }
}
