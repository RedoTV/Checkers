using System.ComponentModel.DataAnnotations;
namespace Checkers.Models
{
    public class Lobby
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string Name { get; set; } = null!;
        public int FirstPlayerId { get; set; }
        public int? SecondPlayerId { get; set; }
        public bool Started { get; set; } = false;
    }
}