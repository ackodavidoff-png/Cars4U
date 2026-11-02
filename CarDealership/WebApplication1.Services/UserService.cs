using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.Services.Core;

namespace WebApplication1.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;
        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            IEnumerable<ApplicationUser> users = dbContext.Users.Include(u => u.Cars).Include(u => u.Town).OrderBy(u => u.FirstName).ThenBy(u => u.LastName).ToArray();
            return users;
        }

        public async Task<ApplicationUser?> GetUserDetailsAsync(int id)
        {
            ApplicationUser? user = await dbContext.Users.Include(u => u.Cars).Include(u => u.Town).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
    }
}
