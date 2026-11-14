using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Services.Core;
using WebApplication1.Data.Models;
using System.Collections;
using WebApplication1.GCommon;
using static WebApplication1.GCommon.ApplicationConstants;
using WebApplication1.ViewModels.TownViewModels;
using WebApplication1.ViewModels.CarViewModels;

namespace WebApplication1.Controllers
{
    public class TownController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ITownService townService;
        public TownController(ApplicationDbContext dbContext, ITownService townService)
        {
            this.dbContext = dbContext;
            this.townService = townService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Town> towns = await townService.GetTownsAsync();
            TownIndexViewModel viewModel = new TownIndexViewModel()
            {
                Towns = towns
            };
            return View(viewModel);
        }
        public async Task<IActionResult> Details(int id, int? pageIndex)
        {
            Town? town = await townService.GetTownDetailsAsync(id);
            IEnumerable<Car> cars = townService.GetAllCarsFromTown(id);
            IEnumerable<CarTableViewModel> allCarsViewModel = cars.Select(c => new CarTableViewModel()
            {
                Id = c.Id,
                Brand = c.Brand,
                Model = c.Model,
                Price = c.Price,
                Image = c.Image,
            });
            PaginatedList<CarTableViewModel> carsPaginated = PaginatedList<CarTableViewModel>.Create(allCarsViewModel.AsQueryable().AsNoTracking(), pageIndex ?? 1, MaxEntitiesPerPage);
            if (town == null)
            {
                return NotFound();
            }
            if (id <= 0)
            {
                return BadRequest();
            }
            TownDetailsViewModel viewModel = new TownDetailsViewModel()
            {
                Name = town.Name,
                Cars = carsPaginated
            };
            return View(viewModel);
        }
    }
}
