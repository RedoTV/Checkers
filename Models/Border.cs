using System.ComponentModel.DataAnnotations;
namespace Checkers.Models
{
    public class Border
    {
        [Required]
        public int Id { get; set; }
        public Checker[] Checkers{ get; set; } = new Checker[24];
        public Border(Lobby lobby)
        {
            Id = lobby.Id;
        }
    }
}