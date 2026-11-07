using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Providers.Entities;
using WebApplication1.Data.Models;
using WebApplication1.Data.Repository.Common;
using WebApplication1.Data.Repository.Contracts;

namespace WebApplication1.Data.Repository
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
