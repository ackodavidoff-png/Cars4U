using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data.Models;
using WebApplication1.Data.Repository.Common;
using WebApplication1.Data.Repository.Contracts;

namespace WebApplication1.Data.Repository
{
    public class TownRepository : BaseRepository<Town>, ITownRepository
    {
        public TownRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
