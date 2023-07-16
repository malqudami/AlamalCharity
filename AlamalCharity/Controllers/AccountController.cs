using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AlamalCharity.Models;
using AlamalCharity.Data;
using AlamalCharity.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AlamalCharity.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDBContext context;

        public AccountController(AppDBContext _context)
        {
            context = _context;
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var query = context.Users
                       .Where(s =>
                            s.USERNAME == loginModel.USERNAME &&
                            s.PASSWORD == loginModel.PASSWORD)
                       .FirstOrDefault();

            if (query != null)
            {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, loginModel.USERNAME),
                    new Claim("OtherProperties","Example Role")

                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {

                    AllowRefresh = true,
                    //IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");
            }

            ViewData["ValidateMessage"] = "اسم المستخدم او كلمة المرور غير صحيح ...";
            return View();
        }
    }
}
