using System.ComponentModel.DataAnnotations;

namespace Checkers.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter Name")]
        [MinLength(3, ErrorMessage ="Name must be more than 3 characters")]
        [MaxLength(30, ErrorMessage ="Name must be less than 30 characters")]
        public string Name { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter password")]
        [MinLength(6, ErrorMessage ="Password must be more than 6 characters")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm the password")]
        [Compare("Password",ErrorMessage = "Passwords don't match")]
        public string PasswordConfirm { get; set; } = null!;
    }
}