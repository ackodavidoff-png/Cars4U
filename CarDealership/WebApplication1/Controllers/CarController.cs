using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.GCommon;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.Services;
using WebApplication1.Services.Core;
using WebApplication1.ViewModels;
using static System.Net.Mime.MediaTypeNames;
using static WebApplication1.GCommon.ApplicationConstraints;
using static WebApplication1.GCommon.ErrorMessages;

namespace WebApplication1.Controllers
{
    public class CarController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ICarService carService;
        public CarController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICarService carService)
        {
            this.context = applicationDbContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.carService = carService;
        }
        //all cars page
        [HttpGet]
        public IActionResult Index(int? pageNumber)
        {
            IEnumerable<Car> cars = carService.ShowAllCars();
            return View(PaginatedList<Car>.Create(cars.AsQueryable().AsNoTracking(), pageNumber ?? 1, MaxEntitiesPerPage));
        }
        //details page
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Car? car = await carService.GetDetailsAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            int userId = int.Parse(userManager.GetUserId(User));
            bool isFavorite = await context.Favorites.AnyAsync(f => f.CarId == id && f.UserId == userId);
            CarDetailsViewModel viewModel = new CarDetailsViewModel()
            {
                Car = car,
                IsFavorite = isFavorite,
            };
            return View(viewModel);
        }
        //create Get method
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //create Post method
        [HttpPost]
        public async Task<IActionResult> Create(CreateCarViewModel carModel, IFormFile? image)
        {
            if (!ModelState.IsValid)
            {
                return View(carModel);
            }
            int currentUserId = GetUserId() ?? 0;
            bool isCarCreated = await carService.CreateCarAsync(carModel, image, currentUserId);
            if (!isCarCreated)
            {
                return BadRequest(BadRequestErrorMessage);
            }
            return RedirectToAction(nameof(Index));
        }
        //search controller
        public IActionResult Search(string? searchText, int? pageNumber)
        {
            if(searchText == null || searchText == "")
            {
                return RedirectToAction(nameof(Index));
            }
            IEnumerable<Car> carsFound = carService.ShowSearchResults(searchText);
            return View(PaginatedList<Car>.Create(carsFound.AsQueryable().AsNoTracking(), pageNumber ?? 1, MaxEntitiesPerPage));
        }
        //edit Get page
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Car? car = context.Cars.Include(c => c.Seller).FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            if (car.SellerId != GetUserId())
            {
                return Unauthorized();
            }
            return View(car);
        }
        //Edit post page
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCarViewModel carModel, IFormFile? image)
        {
            bool isCarEdited = await carService.EditCarAsync(carModel, image, GetUserId() ?? 0, carModel.Id);
            if (!isCarEdited)
            {
                return BadRequest(BadRequestErrorMessage);
            }
            return RedirectToAction(nameof(Details), new Car() { Id = carModel.Id });
        }
        //delete Get page
        [HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            Car? carToDelete = context.Cars.FirstOrDefault(c => c.Id == id);
            if (carToDelete == null)
            {
                return NotFound();
            }
            return View(carToDelete);
        }
        //delete confirmation Post method
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            bool isCarDeleted = await carService.DeleteCarAsync(id);
            if (!isCarDeleted)
            {
                return BadRequest(BadRequestErrorMessage);
            }
            return RedirectToAction(nameof(Index));
        }
        //My cars page reference
        [Authorize]
        [HttpGet]
        public IActionResult MyCars(int? pageNumber)
        {
            IEnumerable<Car> cars = context.Cars.Where(c => c.SellerId == GetUserId()).OrderByDescending(c => c.CreatedOn).ToArray();
            PaginatedList<Car> paginatedCarList = PaginatedList<Car>.Create(cars.AsQueryable().AsNoTracking(), pageNumber ?? 1, MaxEntitiesPerPage);
            return View(paginatedCarList);
        }
        //car image
        public IActionResult Image(int id)
        {
            Car? car = context.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null || car.Image == null)
            {
                return NotFound();
            }
            return File(car.Image, "image/jpeg");
        }
        private int? GetUserId()
        {
            int? userId = null;
            Claim? idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null)
            {
                userId = int.Parse(idClaim.Value);
            }
            return userId;
        }
    }
}
