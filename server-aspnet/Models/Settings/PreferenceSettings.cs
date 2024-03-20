using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class PreferenceSettings
    {
        public int Id { get; set; }
        public string Theme { get; set; }
        public string Language { get; set; }

        // Navigation properties
        public int SettingsId { get; set; }
        public Settings Settings { get; set; }
    }
}