using Checkers.Models;
using Checkers.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Checkers.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class LobbyController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly  ILobbyService _lobbyService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LobbyController(ILogger<HomeController> logger,ILobbyService lobbyService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _lobbyService = lobbyService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult AddLobbyPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddLobbyPage(Lobby lobby)
        {
            Lobby lobbyInfo = _lobbyService.AddLobby(lobby.Name, lobby.Password!, _httpContextAccessor.HttpContext!);
            return View("../Lobby/LobbyWaitingRoom",lobbyInfo);
        }

        [HttpPost]
        public IActionResult LobbyPage(Lobby lobby)
        {
            _lobbyService.AddLobby(lobby.Name, lobby.Password!, _httpContextAccessor.HttpContext!);
            return View();
        }

        [HttpGet]
        public IActionResult ConnectToRoom(string name, string? password)
        {
            Lobby lobbyInfo = _lobbyService.ConnectToLobby(name,password, _httpContextAccessor.HttpContext!);
            return View("../Lobby/LobbyWaitingRoom",lobbyInfo);
        }
    }
}