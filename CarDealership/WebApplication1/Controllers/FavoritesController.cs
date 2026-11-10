using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using System.Security.Claims;
using WebApplication1.GCommon;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.Services.Core;
using static WebApplication1.GCommon.ApplicationConstants;
using static WebApplication1.GCommon.ErrorMessages;

namespace WebApplication1.Controllers
{
    public class FavoritesController : BaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IFavoriteService favoriteService;
        public FavoritesController(ApplicationDbContext context, IFavoriteService favoriteService)
        {
            this.context = context;
            this.favoriteService = favoriteService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(int? pageIndex)
        {
            IQueryable<Favorite> favorites = context.Favorites.Where(f => f.UserId == GetUserId()).Include(f => f.Car).AsNoTracking();
            PaginatedList<Favorite> favoritesPaginated = await PaginatedList<Favorite>.CreateAsync(favorites, pageIndex ?? 1, MaxEntitiesPerPage);
            return View(favoritesPaginated);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(Guid carId)
        {
            bool isCarAddedToFavorites = await favoriteService.AddToFavoritesAsync(GetUserId(), carId);
            if (!isCarAddedToFavorites)
            {
                return BadRequest(BadRequestErrorMessage);
            }
            return RedirectToAction("Index", "Favorites");
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Remove(Guid carId)
        {
            bool isCarRemovedFromFavorites = await favoriteService.RemoveFromFavoritesAsync(GetUserId(), carId);
            if (!isCarRemovedFromFavorites)
            {
                return BadRequest(BadRequestErrorMessage);
            }
            return RedirectToAction("Index", "Favorites");
        }
    }
}
