using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Common;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.ViewModels;
using static System.Net.Mime.MediaTypeNames;
using static WebApplication1.Common.ApplicationConstraints;

namespace WebApplication1.Controllers
{
    public class CarController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public CarController(ApplicationDbContext applicationDbContext)
        {
            this.context = applicationDbContext;
        }
        //all cars page
        public IActionResult Index(int? pageNumber)
        {
            IEnumerable<Car> cars = context.Cars.ToArray();
            return View(PaginatedList<Car>.Create(cars.AsQueryable().AsNoTracking(), pageNumber ?? 1, MaxEntitiesPerPage));
        }
        //details page
        public IActionResult Details(int id)
        {
            Car? car = context.Cars.Include(c => c.Seller).ThenInclude(au => au.Town).FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }
        //create Get method
        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> users = context.Users.Select(au => new SelectListItem()
            {
                Value = au.Id.ToString(),
                Text = au.UserName
            }).ToList();
            ViewBag.Users = users;
            return View();
        }
        //create Post method
        [HttpPost]
        public IActionResult Create(CreateCarViewModel carModel, IFormFile? image)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<SelectListItem> users = context.Users.Select(au => new SelectListItem()
                {
                    Value = au.Id.ToString(),
                    Text = au.UserName
                }).ToList();
                ViewBag.Users = users;
                return View(carModel);
            }
            Car car = new Car()
            {
                Brand = carModel.Brand,
                Model = carModel.Model,
                Year = carModel.Year,
                Price = carModel.Price,
                Mileage = carModel.Mileage,
                EngineType = carModel.EngineType,
                TransmissionType = carModel.TransmissionType,
                HorsePower = carModel.HorsePower,
                State = carModel.State,
                Description = carModel.Description,
                SellerId = carModel.SellerId,
                CreatedOn = DateTime.Now
            };
            if (image != null)
            {
                using MemoryStream memoryStream = new MemoryStream();
                image.CopyTo(memoryStream);
                car.Image = memoryStream.ToArray();
            }
            context.Cars.Add(car);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //search controller
        public IActionResult Search(string? searchText, int? pageNumber)
        {
            if(searchText == null || searchText == "")
            {
                return RedirectToAction(nameof(Index));
            }
            IEnumerable<Car> carsFound = context.Cars.Include(c => c.Seller).Where(c => c.Brand.ToLower().Contains(searchText.ToLower())).ToArray();
            return View(PaginatedList<Car>.Create(carsFound.AsQueryable().AsNoTracking(), pageNumber ?? 1, MaxEntitiesPerPage));
        }
        //edit Get page
        [HttpGet]
        public IActionResult Edit(int id)
        {
            IEnumerable<SelectListItem> users = context.Users.Select(au => new SelectListItem()
            {
                Value = au.Id.ToString(),
                Text = au.UserName
            }).ToList();
            ViewBag.Users = users;
            Car? car = context.Cars.Include(c => c.Seller).FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            //var userId = userManager.GetUserId(User);
            //if (car.SellerId.ToString() != userId)
            //{
            //    return Unauthorized();
            //}
            return View(car);
        }
        //Edit post page
        [HttpPost]
        public IActionResult Edit(int id, EditCarViewModel carModel, IFormFile? image)
        {
            //validating the car
            if (id != carModel.Id)
            {
                return BadRequest();
            }
            Car? car = context.Cars.Include(c => c.Seller).FirstOrDefault(c => c.Id == id);
            //checking if the car exists
            if (car == null)
            {
                return NotFound();
            }
            //var userId = userManager.GetUserId(User);
            //if (car.SellerId.ToString() != userId)
            //{
            //    return Unauthorized();
            //}
            
            //checking if the model state is valid
            if (!ModelState.IsValid)
            {
                ViewBag.Users = context.Users.Select(au => new SelectListItem()
                {
                    Value = au.Id.ToString(),
                    Text = au.UserName
                });
                return View(car);
            }
            if (car.Seller == null)
            {
                return BadRequest();
            }
            //setting the car new properties
            if (image != null)
            {
                using MemoryStream memoryStream = new MemoryStream();
                image.CopyTo(memoryStream);
                car.Image = memoryStream.ToArray();
            }
            car.Brand = carModel.Brand;
            car.Model = carModel.Model;
            car.Year = carModel.Year;
            car.Price = carModel.Price;
            car.Mileage = carModel.Mileage;
            car.EngineType = carModel.EngineType;
            car.TransmissionType = carModel.TransmissionType;
            car.HorsePower = carModel.HorsePower;
            car.State = carModel.State;
            car.Description = carModel.Description;
            car.SellerId = carModel.SellerId;
            car.Seller = context.Users.FirstOrDefault(au => au.Id == carModel.SellerId);
            //saving the changes in the context
            context.SaveChanges();
            return RedirectToAction(nameof(Details), new Car() { Id = car.Id });
        }
        //delete Get page
        [HttpGet]
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
        public IActionResult DeleteConfirmation(int id)
        {
            Car? carToDelete = context.Cars.FirstOrDefault(c => c.Id == id);
            if (carToDelete == null)
            {
                return NotFound();
            }
            context.Cars.Remove(carToDelete);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
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
    }
}
