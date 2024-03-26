using System.ComponentModel.DataAnnotations;
using Server.ValidatorValidationAttribute;

namespace Server.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Username or Email cannot be longer than 50 characters")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters")]
        [Username]
        public string Username { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ProfilePictureURL { get; set; } = null;

        public bool? TwoFactorAuth { get; set; } = false;
    }
}