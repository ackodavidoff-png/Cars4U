using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Common;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using static WebApplication1.Common.ApplicationConstraints;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext context;
        public UserController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index(int? pageNumber)
        {
            IEnumerable<ApplicationUser> users = this.context.ApplicationUsers.Include(au => au.Cars).Include(au => au.Town).OrderBy(au => au.FirstName).ThenBy(au => au.LastName).ToArray();
            return View(PaginatedList<ApplicationUser>.Create(users.AsQueryable().AsNoTracking(), pageNumber ?? 1, MaxEntitiesPerPage));
        }
        public IActionResult Details(int id)
        {
            ApplicationUser? user = context.ApplicationUsers.Include(au => au.Cars).Include(au => au.Town).FirstOrDefault(au => au.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }
}
