using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Providers.Entities;
using WebApplication1.Data.Models;
using WebApplication1.Data.Repository.Contracts.Common;

namespace WebApplication1.Data.Repository.Contracts
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
    }
}
