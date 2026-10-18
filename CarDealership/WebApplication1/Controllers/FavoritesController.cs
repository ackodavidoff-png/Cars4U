using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using System.Security.Claims;
using WebApplication1.Common;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using static WebApplication1.Common.ApplicationConstraints;

namespace WebApplication1.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private readonly ILogger _logger;
        public FavoritesController(ApplicationDbContext context)//, ILogger logger)
        {
            this._context = context;
            //this._logger = logger;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(int? pageIndex)
        {
            IQueryable<Favorite> favorites = _context.Favorites.Where(f => f.UserId == GetUserId()).Include(f => f.Car).AsNoTracking();
            PaginatedList<Favorite> favoritesPaginated = await PaginatedList<Favorite>.CreateAsync(favorites, pageIndex ?? 1, MaxEntitiesPerPage);
            return View(favoritesPaginated);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(int carId)
        {
            if (carId <= 0)
            {
                return BadRequest();
            }
            if (GetUserId() == null)
            {
                return Unauthorized();
            }
            bool doesItemExists = await _context.Favorites.AnyAsync(f => f.CarId == carId);
            try
            {
                if (!doesItemExists)
                {
                    Favorite favCar = new Favorite()
                    {
                        UserId = GetUserId() ?? throw new InvalidOperationException(),
                        CarId = carId
                    };
                    _context.Favorites.Add(favCar);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                //_logger.LogError(ex, "Invalid operation");
                //StatusMessage = "An error occurred while saving customer data!";
            }
            return RedirectToAction("Index", "Favorites");
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Remove(int carId)
        {
            if (carId <= 0)
            {
                return BadRequest();
            }
            if (GetUserId() == null)
            {
                return Unauthorized();
            }
            try
            {
                Favorite? favorite = await _context.Favorites.Include(f => f.Car).ThenInclude(f => f.Seller).FirstOrDefaultAsync(f => f.UserId == GetUserId() && f.CarId == carId);
                bool carExists = favorite != null;
                if (carExists)
                {
                    _context.Favorites.Remove(favorite);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Invalid operation");
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index", "Favorites");
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
