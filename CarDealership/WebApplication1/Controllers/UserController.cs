using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.GCommon;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using static WebApplication1.GCommon.ApplicationConstants;
using WebApplication1.Services.Core;
using WebApplication1.ViewModels.UserViewModels;

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
            IEnumerable<UserIndexPageViewModel> viewModel = users.Select(u => new UserIndexPageViewModel()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                CarsCount = u.Cars.Count
            });
            return View(PaginatedList<UserIndexPageViewModel>.Create(viewModel.AsQueryable().AsNoTracking(), pageNumber ?? 1, MaxEntitiesPerPage));
        }
        public async Task<IActionResult> Details(string id)
        {
            ApplicationUser? user = await userService.GetUserDetailsAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            UserDetailsViewModel viewModel = new UserDetailsViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                TownName = user.Town.Name,
                Cars = user.Cars
            };
            return View(viewModel);
        }
    }
}
