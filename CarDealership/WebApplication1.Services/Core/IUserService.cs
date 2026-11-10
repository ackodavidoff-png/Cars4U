using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data.Models;

namespace WebApplication1.Services.Core
{
    public interface IUserService
    {
        IEnumerable<ApplicationUser> GetAllUsers();
        Task<ApplicationUser?> GetUserDetailsAsync(string id);
    }
}
