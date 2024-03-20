using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class Settings
    {
        [Key]
        public int Id { get; set; }

        // Navigation properties
        public int UserId { get; set; }
        public User User { get; set; }
        public PrivacySettings PrivacySettings { get; set; }
        public SecuritySettings SecuritySettings { get; set; }
        public PreferenceSettings PreferenceSettings { get; set; }
        public NotificationSettings NotificationSettings { get; set; }

        public LoginLocations LoginLocations { get; set; }

    }
}