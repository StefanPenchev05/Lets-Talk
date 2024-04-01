using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class TempData
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string ProfilePictureURL { get; set; } = null;

        public bool? TwoFactorAuth { get; set; } = false;

        [Required]
        public string VerificationCode { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public TempData()
        {
            ExpiryDate = DateTime.UtcNow.AddMinutes(15);
        }
    }
}