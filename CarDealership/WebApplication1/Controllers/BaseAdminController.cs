using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static WebApplication1.GCommon.ApplicationConstants;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = AdminDefaultRole)]
    [AutoValidateAntiforgeryToken]
    public class BaseAdminController : Controller
    {
    }
}
