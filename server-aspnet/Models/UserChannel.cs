using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class UserChannel
    {
        public int UserChannelId { get; set; }

        // Foreign keys
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public int RoleId { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Channel Channel { get; set; }
        public Role Role { get; set; }

        // New TimeSpan
        public TimeSpan TimeInChannel { get; set; }
    }
}