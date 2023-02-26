using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Checkers.Hubs
{
    public class LobbyHub : Hub
    {
        [Authorize]
        public async Task SynchronizeLobby(){

        }
    }
}