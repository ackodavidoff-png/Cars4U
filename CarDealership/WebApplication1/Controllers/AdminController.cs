using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Models;
using WebApplication1.Data.Repository;
using WebApplication1.Data.Repository.Contracts;
using WebApplication1.Services.Core;
using WebApplication1.ViewModels.AdminPanelViewModels;
using static WebApplication1.GCommon.ApplicationConstants;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;
        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [Authorize(Roles = AdminDefaultRole)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = AdminDefaultRole)]
        [HttpGet]
        public IActionResult TownManager()
        {
            IEnumerable<Town> towns = adminService.GetAllTowns();
            IQueryable<TownManagerViewModel> viewModel = towns.Select(t => new TownManagerViewModel()
            {
                Id = t.Id,
                Name = t.Name,
                UsersCount = t.Users.Count,
                CarsCount = t.Users.Sum(u => u.Cars.Count)
            }).AsQueryable();
            return View(viewModel);
        }
        [Authorize(Roles = AdminDefaultRole)]
        [HttpGet]
        public async Task<IActionResult> DeleteTown(int id)
        {
            Town? town = await adminService.GetTownByIdAsync(id);
            if (town == null)
            {
                return NotFound();
            }
            if (town.Id != id)
            {
                return BadRequest();
            }
            if (town.Users.Any())
            {
                return BadRequest();
            }
            DeleteTownConfirmationViewModel viewModel = new DeleteTownConfirmationViewModel()
            {
                Id = town.Id,
                Name = town.Name,
            };
            return View(viewModel);
        }
        [Authorize(Roles = AdminDefaultRole)]
        [HttpPost]
        public async Task<IActionResult> DeleteTownConfirmation(int id)
        {
            bool isTownDeleted = await adminService.DeleteTownAsync(id);
            if (!isTownDeleted)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(TownManager));
        }
        [Authorize(Roles = AdminDefaultRole)]
        [HttpPost]
        public async Task<IActionResult> CreateTownAsync(string townName)
        {
            if (townName == null)
            {
                return BadRequest();
            }
            bool isTownCreated = await adminService.CreateTownAsync(townName);
            if (!isTownCreated)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(TownManager));
        }
        [Authorize(Roles = AdminDefaultRole)]
        [HttpPost]
        public async Task<IActionResult> EditTownName(int id, string name)
        {
            bool isTownNameEdited = await adminService.EditTownNameAsync(id, name);
            if (!isTownNameEdited)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(TownManager));
        }
    }
}
