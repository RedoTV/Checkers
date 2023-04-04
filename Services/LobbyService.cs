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

        public Lobby AddLobby(string name, string password){
            int findedLobbyCount = CheckersDbContext.Lobbies.Where(l => l.Name == name).Count();
            if(findedLobbyCount > 0) 
                throw new Exception("lobby with this name already exist");

            string? psw = null;
            if(password != null)
                psw = LoginService.ComputeStringToSha256Hash(password);
            
            Lobby newLobby = new Lobby(){
                Name = name,
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

        public Lobby ConnectToLobby(string nameOfLobby, string? password, HttpContext context)
        {
            if(password != null)
            {
                string psw = LoginService.ComputeStringToSha256Hash(password);
                IEnumerable<Lobby> lobbies = CheckersDbContext.Lobbies.Where(l => l.Name == nameOfLobby && l.Password == psw);
                if(lobbies.Count() > 1)
                    throw new Exception("Many lobbies with this name and a password");
                else if(lobbies.Count() == 0) 
                    throw new Exception("0 lobbies with this name and password");
                else if(lobbies.Count() == 1)
                {
                    Lobby lobbyForConnect = lobbies.First();

                    if(lobbyForConnect.FirstPlayerName == null)
                    {
                        lobbyForConnect.FirstPlayerName = context.User.Identity!.Name!;
                    }
                    else if(lobbyForConnect.SecondPlayerName == null && lobbyForConnect.FirstPlayerName != context.User.Identity!.Name)
                    {
                        lobbyForConnect.SecondPlayerName = context.User.Identity!.Name;
                    }
                    
                    CheckersDbContext.SaveChangesAsync();
                }
                return lobbies.First();
            } 
            else
            {
                IEnumerable<Lobby> lobbies = CheckersDbContext.Lobbies.Where(l => l.Name == nameOfLobby);
                if(lobbies.Count() > 1)
                {
                    throw new Exception("Many lobbies with this name and a password");
                }
                else if(lobbies.Count() == 0)
                {
                    throw new Exception("0 lobbies with this name and password");
                }
                else
                {
                    Lobby lobbyForConnect = lobbies.First();

                    if(lobbyForConnect.FirstPlayerName == null)
                    {
                        lobbyForConnect.FirstPlayerName = context.User.Identity!.Name!;
                    }
                    else if(lobbyForConnect.SecondPlayerName == null && lobbyForConnect.FirstPlayerName != context.User.Identity!.Name)
                    {
                        lobbyForConnect.SecondPlayerName = context.User.Identity!.Name;
                    }
                    
                     CheckersDbContext.SaveChanges();
                }
                Lobby result = lobbies.First();
                return result;
            }
        }
    }
}