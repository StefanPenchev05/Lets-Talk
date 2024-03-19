namespace Server.Models
{
    public class Settings
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DarmModeSetting DarkMode { get; set; } = DarmModeSetting.System;
        public string Language { get; set; }
        public bool Notifications { get; set; } = true;
        public bool AllowDirectMessages { get; set; }
        public bool ShowOnlineStatus { get; set; }

        // Navigation properties
        public User User { get; set; }
    }

    public enum DarmModeSetting
    {
        Dark,
        Light,
        System
    }
}