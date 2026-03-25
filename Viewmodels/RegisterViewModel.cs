using System.ComponentModel.DataAnnotations;

namespace Harmony.Viewmodels
{
    public class RegisterViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{12,}$", ErrorMessage = "Password must be at least 12 characters long, contain at least one uppercase letter and one number.")]
        public string Password { get; set; }
    }
}
