using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        // Foreign keys
        public int UserId { get; set; }
        public int ChannelId { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Channel channel { get; set; }
    }
}