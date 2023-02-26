using System.ComponentModel.DataAnnotations;

namespace Checkers.Models
{
    public class Checker
    {
        public Colors Color { get; set; }
        public Types Type { get; set; } = Types.Pawn;
        [Range(0,7)]
        public int Row { get; set; }
        [Range(0,7)]
        public int Column { get; set; }
    }
    public enum Colors
    {
        White,
        Black
    }
    public enum Types
    {
        Pawn,
        King
    } 
}