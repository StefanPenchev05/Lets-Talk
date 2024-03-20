using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Channel
    {
        public int ChannelId { get; set; }
        public string Name { get; set; } = "Chat Group";
        public byte[] Image { get; set; } = null;

        // Naviagtion properties
        public ICollection<Message> Messages { get; set; }
        public ICollection<UserChannel> UserChannels { get; set; }
    }
}