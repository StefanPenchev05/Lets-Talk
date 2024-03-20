using System.ComponentModel.DataAnnotations;

namespace Server.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username of Email is required")]
        [StringLength(50, ErrorMessage = "Username or Email cannot be longer than 50 characters")]
        public string UsernameOrEmail { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 6 and 100 characters")]
        public string Password { get; set; }
    }
}