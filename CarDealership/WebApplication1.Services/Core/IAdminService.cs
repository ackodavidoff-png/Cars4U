using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data.Models;

namespace WebApplication1.Services.Core
{
    public interface IAdminService
    {
        IQueryable<Town> GetAllTowns();
        Task<Town?> GetTownByIdAsync(int id);
        Task<bool> DeleteTownAsync(int id);
        Task<bool> CreateTownAsync(string name);
        Task<bool> EditTownNameAsync(int id, string name);
        IQueryable<ApplicationUser> GetAllUsers();
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<bool> DeleteUserAsync(string id);
    }
}
