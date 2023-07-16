using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlamalCharity.Controllers
{
    [Authorize]
    public class SubsController : Controller
    {
        public IActionResult Subs()
        {
            return View();
        }
    }
}
