using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Checkers.Hubs
{
    
    public class LobbyHub : Hub
    {
        public async Task SynchronizeLobby(int fromRow, int fromColumn, int toRow, int toColumn){
            await Clients.Caller.SendAsync("Synchonize", fromRow , fromColumn , toRow, toColumn);
        }
        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Receive", message);
        }
    }
}