namespace Server.Models
{
    public class Settings
    {
        public int Id { get; set; }
        public DarkModeSetting DarkMode { get; set; } = DarkModeSetting.System;
        public bool TwoFactorAuth { get; set; } = false;
        public string Language { get; set; }
        public bool Notifications { get; set; } = true;
        public bool AllowDirectMessages { get; set; }
        public bool ShowOnlineStatus { get; set; }

        // Navigation properties
        public int UserId { get; set; }
        public User User { get; set; }
    }

    public enum DarkModeSetting
    {
        Dark,
        Light,
        System
    }
}