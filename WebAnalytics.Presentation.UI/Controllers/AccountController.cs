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
        public async Task<IActionResult> Login(string login, string password, string returnUrl = null)
        {
            if (ValidateLogin(login, password))
            {
                var claims = new List<Claim>
                {
                    new Claim("user", login),
                    new Claim("role", "Admin")
                };

                await HttpContext.SignInAsync(
                    new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")),
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddHours(1),
                        IsPersistent = true
                    });

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/");
                }
            }
            return Redirect("AccessDenied");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private bool ValidateLogin(string login, string password)
        {
            return _identityContext.Users.Where(u => u.Login == login && u.Password == password).Any();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}