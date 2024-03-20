using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class PrivacySettings
    {
        public int Id { get; set; }
        public bool AllowDirectMessages { get; set; }
        public bool ShowOnlineStatus { get; set; }

        // Navigation properties
        public int SettingsId { get; set; }
        public Settings Settings { get; set; }
    }
}