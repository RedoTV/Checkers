using Checkers.Models;
using Checkers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Checkers.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ILoginService LoginService;
        private readonly IHttpContextAccessor HttpContextAccessor;
        public AuthController(ILogger<AuthController> logger, IHttpContextAccessor httpContextAccessor, ILoginService loginService)
        {
            _logger = logger;
            LoginService = loginService;
            HttpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            { 
                await LoginService.Registry(HttpContextAccessor.HttpContext!, model);
                return Redirect("/");
            }
            else return View(model);
        }

        [HttpGet]
        public IActionResult LogIn() => View();

        [HttpPost]
        public async Task<IActionResult> LogIn(SignInViewModel model)
        {
            await LoginService.LogIn(HttpContextAccessor.HttpContext!, model);
            return Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}