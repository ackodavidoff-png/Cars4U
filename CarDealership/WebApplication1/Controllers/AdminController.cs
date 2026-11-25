using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Models;
using WebApplication1.Data.Repository;
using WebApplication1.Data.Repository.Contracts;
using WebApplication1.Data.Seeding.Utilities;
using WebApplication1.GCommon;
using WebApplication1.Services.Core;
using WebApplication1.ViewModels.AdminPanelViewModels;
using static WebApplication1.GCommon.ApplicationConstants;
using static WebApplication1.GCommon.NotificationMessages;

namespace WebApplication1.Controllers
{
    public class AdminController : BaseAdminController
    {
        private readonly IAdminService adminService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AdminController(IAdminService adminService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.adminService = adminService;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
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
        [HttpGet]
        public IActionResult UserManager(int? pageNumber)
        {
            IQueryable<ApplicationUser> users = adminService.GetAllUsers();
            IQueryable<UserManagerViewModel> viewModel = users.Select(u => new UserManagerViewModel()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                CarsCount = u.Cars.Count
            });
            PaginatedList<UserManagerViewModel> viewModelPaginated = PaginatedList<UserManagerViewModel>.Create(viewModel, pageNumber ?? 1, MaxEntitiesPerPage);
            return View(viewModelPaginated);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            ApplicationUser? user = await adminService.GetUserByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            DeleteUserViewModel viewModel = new DeleteUserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUserConfirmation(string id)
        {
            bool isUserDeleted = await adminService.DeleteUserAsync(id);
            if (!isUserDeleted)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(UserManager));
        }
        [HttpGet]
        public IActionResult CarManager(int? pageNumber)
        {
            IQueryable<Car> cars = adminService.GetAllCars();
            IQueryable<CarManagerViewModel> viewModel = cars.Select(c => new CarManagerViewModel()
            {
                Id = c.Id,
                Image = c.Image,
                Brand = c.Brand,
                Model = c.Model,
                SellerId = c.SellerId,
                SellerFirstName = c.Seller.FirstName,
                SellerLastName = c.Seller.LastName,
                SellerUsername = c.Seller.UserName,
                Price = c.Price,
                CreatedOn = c.CreatedOn
            });
            PaginatedList<CarManagerViewModel> viewModelPaginated = PaginatedList<CarManagerViewModel>.Create(viewModel, pageNumber ?? 1, MaxEntitiesPerPage);
            return View(viewModelPaginated);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCar(Guid Id)
        {
            Car? car = await adminService.GetCarByIdAsync(Id);
            if (car == null)
            {
                return NotFound();
            }
            DeleteCarViewModel viewModel = new DeleteCarViewModel()
            {
                Id = car.Id,
                Image = car.Image,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Price = car.Price,
                CreatedOn = car.CreatedOn,
                SellerId = car.SellerId,
                SellerFirstName= car.Seller.FirstName,
                SellerLastName= car.Seller.LastName,
                SellerUsername = car.Seller.UserName,
                TownName = car.Seller.Town.Name,
                Email = car.Seller.Email,
                PhoneNumber = car.Seller.PhoneNumber,
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCarConfirmation(Guid id)
        {
            bool isCarDeleted = await adminService.DeleteCarAsync(id);
            if (!isCarDeleted)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(CarManager));
        }
        [HttpGet]
        public async Task<IActionResult> UserRoleChanger()
        {
            IEnumerable<UserRoleManagerViewModel> viewModel = await adminService.GetAllUsers().AsSplitQuery().Select(u => new UserRoleManagerViewModel()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName
            }).ToArrayAsync();
            foreach (UserRoleManagerViewModel model in viewModel)
            {
                ApplicationUser user = (await adminService.GetUserByIdAsync(model.Id))!;
                model.Roles = await userManager.GetRolesAsync(user);
            }
            ViewData["ApplicationRoles"] = IdentitySeeder.DefaultRoles;
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(UserRoleInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            ApplicationUser? dbUser = await userManager.FindByIdAsync(inputModel.UserId);
            if (dbUser == null)
            {
                TempData[ErrorNotificationKey] = UserNotExists;
                return RedirectToAction(nameof(Index));
            }

            IdentityRole? dbRole = await roleManager.FindByNameAsync(inputModel.RoleName);
            if (dbRole == null)
            {
                TempData[ErrorNotificationKey] = RoleAssignNotEists;
                return RedirectToAction(nameof(Index));
            }

            bool userAlreadyInRole = await userManager.IsInRoleAsync(dbUser, inputModel.RoleName);
            if (userAlreadyInRole)
            {
                TempData[WarningNotificationKey] = UserAlreadyinRole;
                return RedirectToAction(nameof(Index));
            }

            IdentityResult result = await userManager.AddToRoleAsync(dbUser, inputModel.RoleName);
            if (!result.Succeeded)
            {
                TempData[ErrorNotificationKey] = UnexpectedError;
                return RedirectToAction(nameof(Index));
            }

            TempData[SuccessNotificationKey] = RoleAssigned;

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> RemoveRole(UserRoleInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            ApplicationUser? dbUser = await userManager.FindByIdAsync(inputModel.UserId);
            if (dbUser == null)
            {
                TempData[ErrorNotificationKey] = UserNotExists;
                return RedirectToAction(nameof(Index));
            }

            IdentityRole? dbRole = await roleManager.FindByNameAsync(inputModel.RoleName);
            if (dbRole == null)
            {
                TempData[ErrorNotificationKey] = RoleRemoveNotExists;
                return RedirectToAction(nameof(Index));
            }

            bool userAlreadyInRole = await userManager.IsInRoleAsync(dbUser, inputModel.RoleName);
            if (!userAlreadyInRole)
            {
                TempData[WarningNotificationKey] = UserNotInRole;
                return RedirectToAction(nameof(Index));
            }

            IdentityResult result = await userManager.RemoveFromRoleAsync(dbUser, inputModel.RoleName);
            if (!result.Succeeded)
            {
                TempData[ErrorNotificationKey] = UnexpectedError;
                return RedirectToAction(nameof(Index));
            }

            TempData[SuccessNotificationKey] = RoleRemoved;

            return RedirectToAction(nameof(Index));
        }
    }
}
