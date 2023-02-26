using Checkers.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Checkers.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class LobbyController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public LobbyController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult AddLobby()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddLobby(Lobby lobby)
        {
            return View();
        }

        [HttpPost]
        public IActionResult LobbyPage()
        {
            return View();
        }
    }
}