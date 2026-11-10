using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.GCommon;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using static WebApplication1.GCommon.ApplicationConstants;
using WebApplication1.Services.Core;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IUserService userService;
        public UserController(ApplicationDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }
        public IActionResult Index(int? pageNumber)
        {
            IEnumerable<ApplicationUser> users = userService.GetAllUsers();
            return View(PaginatedList<ApplicationUser>.Create(users.AsQueryable().AsNoTracking(), pageNumber ?? 1, MaxEntitiesPerPage));
        }
        public async Task<IActionResult> Details(string id)
        {
            ApplicationUser? user = await userService.GetUserDetailsAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }
}
