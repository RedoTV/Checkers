using Checkers.Models;

namespace Checkers.Services
{
    public interface ILobbyService
    {
        public IEnumerable<Lobby> GetLobbies();
        public Lobby AddLobby(string name, int firstPlayerId);
        public string DeleteLobby(int id);
    }
}