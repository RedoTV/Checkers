using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Checkers.Hubs
{
    
    public class LobbyHub : Hub
    {
        public async Task SynchronizeLobby(){
            
        }
        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Receive", message);
        }
    }
}