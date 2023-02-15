using Checkers.Models;
using Microsoft.EntityFrameworkCore;

namespace Checkers.Data
{
    public class CheckersDbContext : DbContext
    {
        public CheckersDbContext(DbContextOptions<CheckersDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
        public DbSet<Lobby> Lobbies { get; set; } = null!;
        public DbSet<User> Users {get; set;} = null!;
    }
}