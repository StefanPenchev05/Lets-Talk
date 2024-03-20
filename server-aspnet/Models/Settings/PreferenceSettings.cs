using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class PreferenceSettings
    {
        public int Id { get; set; }
        public Theme Theme { get; set; } = Theme.System;
        public string Language { get; set; } = "English";

        // Navigation properties
        public int SettingsId { get; set; }
        public Settings Settings { get; set; }
    }

    public enum Theme
    {
        Dark,
        Light,
        System
    }
}