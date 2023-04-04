using System.ComponentModel.DataAnnotations;
namespace Checkers.Models
{
    public class Lobby
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(18)]
        public string Name { get; set; } = null!;
        public string? FirstPlayerName { get; set; }
        public string? SecondPlayerName { get; set; }
        public string? FirstPlayerId { get; set; } = null!;
        public string? SecondPlayerId { get; set; }
        public bool Started { get; set; } = false;
        public string? Password { get; set; }
    }
}