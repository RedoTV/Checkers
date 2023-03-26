using System.Security.Cryptography;
using Checkers.Data;
using Checkers.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Checkers.Services
{
    public class LobbyService : ILobbyService
    {
        private readonly IWebHostEnvironment Env;
        private readonly CheckersDbContext CheckersDbContext;
        public LobbyService(IWebHostEnvironment env, CheckersDbContext checkersDbContext)
        {
            Env = env;
            CheckersDbContext = checkersDbContext;
        }

        public IEnumerable<Lobby> GetLobbies(){
            List<Lobby> lobbies = CheckersDbContext.Lobbies.ToList();
            return lobbies;
        }

        public Lobby AddLobby(string name, string password, HttpContext context){
            int findedLobbyCount = CheckersDbContext.Lobbies.Where(l => l.Name == name).Count();
            if(findedLobbyCount > 0) 
                throw new Exception("lobby with this name already exist");

            string? psw = null;
            if(password != null)
                psw = LoginService.ComputeStringToSha256Hash(password);
            
            Lobby newLobby = new Lobby(){
                Name = name,
                FirstPlayerName = context.User.Identity!.Name!,
                Password = psw
            }; 
            CheckersDbContext.Lobbies.Add(newLobby);
            CheckersDbContext.SaveChanges();
            return newLobby;
        }

        public string DeleteLobby(int id){
            List<Lobby> lobbies = CheckersDbContext.Lobbies.ToList();
            Lobby? deletedLobby = lobbies.Find( lobby => lobby.Id == id);
            if(deletedLobby == null){
                return null!;
            }
            else{
                lobbies.Remove(deletedLobby);
                return deletedLobby.Name;
            }
        }

        public Lobby ConnectToLobby(string name, string? password, HttpContext context)
        {
            if(password != null)
            {
                string psw = LoginService.ComputeStringToSha256Hash(password);
                Lobby lobbyForConnect = CheckersDbContext.Lobbies.Where(l => l.Name == name && l.Password == psw).First();
                if(lobbyForConnect.FirstPlayerName == context.User.Identity!.Name)
                    throw new Exception("User with this nickname already contains in lobby");
                
                lobbyForConnect.SecondPlayerName = context.User.Identity!.Name;
                CheckersDbContext.SaveChangesAsync();
                return lobbyForConnect;
            } 
            else
            {
                Lobby lobbyForConnect = CheckersDbContext.Lobbies.Where(l => l.Name == name).First();
                if(lobbyForConnect.FirstPlayerName == context.User.Identity!.Name)
                    throw new Exception("User with this nickname already contains in lobby");
                
                lobbyForConnect.SecondPlayerName = context.User.Identity!.Name;
                CheckersDbContext.SaveChangesAsync();
                return lobbyForConnect;
            }
        }
    }
}