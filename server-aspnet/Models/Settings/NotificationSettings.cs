using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class NotificationSettings
    {
        public int Id { get; set; }
        public bool ReceiveEmailNotifications { get; set; }
        public bool ReceivePushNotifications { get; set; }

        // Navigation properties
        public int SettingsId { get; set; }
        public Settings Settings { get; set; }
    }
}