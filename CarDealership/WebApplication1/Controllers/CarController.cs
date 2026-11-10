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
using static System.Net.Mime.MediaTypeNames;
using static WebApplication1.GCommon.ApplicationConstants;
using static WebApplication1.GCommon.ErrorMessages;
using WebApplication1.ViewModels.CarViewModels;

namespace WebApplication1.Controllers
{
    public class CarController : BaseController
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
        public IActionResult Index(int? pageNumber, string sortOrder)
        {
            IEnumerable<Car> cars = carService.ShowAllCars();
            //CarIndexViewModel viewModel = new CarIndexViewModel()
            //{
            //    Cars = PaginatedList<Car>.Create(cars.AsQueryable().AsNoTracking(), pageNumber ?? 1, MaxEntitiesPerPage),
            //    SortOrder = sortOrder
            //};
            return View(PaginatedList<Car>.Create(cars.AsQueryable().AsNoTracking(), pageNumber ?? 1, MaxEntitiesPerPage));
        }
        //details page
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            Car? car = await carService.GetDetailsAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            string userId = userManager.GetUserId(User)!;
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
            string currentUserId = GetUserId();
            if (currentUserId == null)
            {
                return Unauthorized();
            }
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
            SearchCarsViewModel viewModel = new SearchCarsViewModel()
            {
                SearchText = searchText,
                Cars = PaginatedList<Car>.Create(carsFound.AsQueryable().AsNoTracking(), pageNumber ?? 1, MaxEntitiesPerPage)
            };
            return View(viewModel);
        }
        //edit Get page
        [Authorize]
        [HttpGet]
        public IActionResult Edit(Guid id)
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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, EditCarViewModel carModel, IFormFile? image)
        {
            string userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            bool isCarEdited = await carService.EditCarAsync(carModel, image, userId, carModel.Id);
            if (!isCarEdited)
            {
                return BadRequest(BadRequestErrorMessage);
            }
            return RedirectToAction(nameof(Details), new Car() { Id = carModel.Id });
        }
        //delete Get page
        [HttpGet]
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            Car? carToDelete = context.Cars.FirstOrDefault(c => c.Id == id);
            if (carToDelete == null)
            {
                return NotFound();
            }
            DeleteCarViewModel viewModel = new DeleteCarViewModel()
            {
                Id = carToDelete.Id,
                Image = carToDelete.Image,
                Brand = carToDelete.Brand,
                Model = carToDelete.Model,
                Year = carToDelete.Year,
                Price = carToDelete.Price
            };
            return View(viewModel);
        }
        //delete confirmation Post method
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
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
        public IActionResult Image(Guid id)
        {
            Car? car = context.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null || car.Image == null)
            {
                return NotFound();
            }
            return File(car.Image, "image/jpeg");
        }
    }
}
