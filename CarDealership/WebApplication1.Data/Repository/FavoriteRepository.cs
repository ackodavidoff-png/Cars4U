using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data.Models;
using WebApplication1.Data.Repository.Common;
using WebApplication1.Data.Repository.Contracts;

namespace WebApplication1.Data.Repository
{
    public class FavoriteRepository : BaseRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
