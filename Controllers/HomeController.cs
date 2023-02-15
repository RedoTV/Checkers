using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Checkers.Models;
using Checkers.Services;

namespace Checkers.Controllers;

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
    [HttpPost]
    public IActionResult AddLobby(string name,int firstPlayerId){

        Lobby addedLobby = LobbyService.AddLobby(name , firstPlayerId);
        return Ok(addedLobby);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
