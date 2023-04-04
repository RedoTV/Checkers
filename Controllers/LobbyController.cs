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
            Lobby lobbyInfo = _lobbyService.AddLobby(lobby.Name, lobby.Password!);
            return RedirectToAction("ConnectToRoom", lobbyInfo);
        }

        [HttpPost]
        public IActionResult LobbyPage(Lobby lobby)
        {
            _lobbyService.AddLobby(lobby.Name, lobby.Password!);
            return View();
        }

        [HttpGet]
        public IActionResult ConnectToRoom(Lobby? _lobby, string? nameOfLobby, string? password)
        {
            Lobby lobbyInfo = new Lobby();
            if(_lobby!.Name != null)
            {
                lobbyInfo = _lobbyService.ConnectToLobby(_lobby.Name!, password, _httpContextAccessor.HttpContext!);
            }
            else if(nameOfLobby != null)
            {
                lobbyInfo = _lobbyService.ConnectToLobby(nameOfLobby!, password, _httpContextAccessor.HttpContext!);
            }
            return View("../Lobby/LobbyWaitingRoom",lobbyInfo);
        }
    }
}