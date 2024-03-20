using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class LoginLocations
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Location { get; set; }

        [Required]
        public DateTime LoginTime { get; set; }

        [MaxLength(100)]
        public string LoginBrowser { get; set; }

        [MaxLength(100)]
        public string LoginDeviceName { get; set; }

        [MaxLength(100)]
        public string LoginDeviceOperatingSystem { get; set; }

        // Navigation properties
        [Required]
        public int SettingsId { get; set; }
        public Settings Settings { get; set; }
    }
}