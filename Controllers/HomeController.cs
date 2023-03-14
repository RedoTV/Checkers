using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Checkers.Models;
using Checkers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Checkers.Controllers;

[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ILobbyService LobbyService;
    public HomeController(ILogger<HomeController> logger,ILobbyService lobbyService)
    {
        _logger = logger;
        LobbyService = lobbyService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(LobbyService.GetLobbies());
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
