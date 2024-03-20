namespace Server.Models
{
    public class Settings
    {
        public int Id { get; set; }

        // Navigation properties
        public int UserId { get; set; }
        public User User { get; set; }
        public PrivacySettings PrivacySettings { get; set; }
        public SecuritySettings SecuritySettings { get; set; }
        public PreferenceSettings PreferenceSettings { get; set; }
        public NotificationSettings NotificationSettings { get; set; }

    }
}