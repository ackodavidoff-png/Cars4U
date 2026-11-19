using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static WebApplication1.GCommon.ApplicationConstants;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = AdminDefaultRole)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
