using Checkers.Data;
using Checkers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Checkers.Hubs
{
    
    public class LobbyHub : Hub
    {
        private readonly CheckersDbContext CheckersDbContext;
        public LobbyHub(CheckersDbContext dbContext)
        {
            CheckersDbContext = dbContext;
        }
        public async Task SynchronizeLobby(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            await Clients.Caller.SendAsync("Synchonize", fromRow , fromColumn , toRow, toColumn);
        }
        public async Task SendMessage(string playerName, string message)
        {
            Lobby currentLobby = CheckersDbContext.Lobbies.Where(l => l.FirstPlayerName == playerName || l.SecondPlayerName == playerName).First();
            string firstConnectionId = currentLobby.FirstPlayerId!;
            string? secondConnectionId = currentLobby.SecondPlayerId!;
            await Clients.Client(firstConnectionId).SendAsync("RecieveMessage", message, playerName);
            if(secondConnectionId != null)
                await Clients.Client(secondConnectionId).SendAsync("RecieveMessage", message, playerName);
        }

        public async Task SyncUserId(string playerName)
        {
            Lobby currentLobby = CheckersDbContext.Lobbies.Where(l=>l.FirstPlayerName == playerName || l.SecondPlayerName == playerName).First();

            if(playerName == currentLobby.FirstPlayerName)
            {
                currentLobby.FirstPlayerId = Context.ConnectionId;
            }
            else if(playerName == currentLobby.SecondPlayerName)
            {
                currentLobby.SecondPlayerId = Context.ConnectionId;
                await Clients.Client(currentLobby.FirstPlayerId!).SendAsync("RecieveSecondPlayer",playerName);
            }

            await CheckersDbContext.SaveChangesAsync();
        }
    }
}