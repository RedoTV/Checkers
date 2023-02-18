using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Checkers.Controllers
{
    [Authorize]
    public class LobbyController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public LobbyController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AddLobby()
        {
            return View();
        }
    }
}