using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Channel
    {
        [Key]
        public int ChannelId { get; set; }
        public string Name { get; set; } = "Chat Group";
        public string ImageURL { get; set; } = null;

        // Naviagtion properties
        public ICollection<Message> Messages { get; set; }
        public ICollection<UserChannel> UserChannels { get; set; }
    }
}