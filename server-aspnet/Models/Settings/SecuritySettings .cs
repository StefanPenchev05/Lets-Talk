using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class SecuritySettings
    {
        public int Id { get; set; }
        public bool TwoFactorAuth { get; set; } = false;
        public string? TwoFactorAuthCode { get; set; } = null;
        public DateTime? LastPasswordChange { get; set; }
        public int FailedLoginAttempts { get; set; } = 0;
        public DateTime? AccountLockoutEnd { get; set; }

        // Navigation properties
        public int SettingsId { get; set; }
        public Settings Settings { get; set; }
    }
}