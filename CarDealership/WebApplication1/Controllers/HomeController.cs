using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Data.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            //ViewData["Message"] = "This page is still in development";
            ViewBag.Message = "This page is still in development";
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode)
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            switch (statusCode)
            {
                case StatusCodes.Status400BadRequest:
                    return View("BadRequest");
                case StatusCodes.Status401Unauthorized:
                    return View("Unauthorized");
                case StatusCodes.Status403Forbidden:
                    return View("Forbidden");
                case StatusCodes.Status404NotFound:
                    return View("NotFound");
                case StatusCodes.Status500InternalServerError:
                    return View("InternalServerError");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
