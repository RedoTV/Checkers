using System.ComponentModel.DataAnnotations;
namespace Checkers.Models
{
    public class Lobby
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(10)]
        public string Name { get; set; } = null!;
        public string FirstPlayerName { get; set; } = null!;
        public string? SecondPlayerId { get; set; }
        public bool Started { get; set; } = false;
    }
}