using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        // Foreign keys
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ChannelId { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Channel Channel { get; set; }
    }
}