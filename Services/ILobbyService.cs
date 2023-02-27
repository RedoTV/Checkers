using Checkers.Models;

namespace Checkers.Services
{
    public interface ILobbyService
    {
        public IEnumerable<Lobby> GetLobbies();
        public Lobby AddLobby(string name, string firstPlayerName);
        public string DeleteLobby(int id);
    }
}