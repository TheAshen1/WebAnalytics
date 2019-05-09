using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebAnalytics.Misc.Identity;

namespace WebAnalytics.Presentation.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IdentityContext _identityContext;

        public AccountController(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl = null)
        {
            return View(returnUrl as object);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string returnUrl = null)
        {
            if (ValidateLogin(userName, password))
            {
                var claims = new List<Claim>
                {
                    new Claim("user", userName),
                    new Claim("role", "Admin")
                };

                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/");
                }
            }
            return View(returnUrl);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private bool ValidateLogin(string userName, string password)
        {
            return _identityContext.Users.Where(u => u.Login == userName && u.Password == password).Any();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}