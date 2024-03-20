using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class UserChannel
    {
        [Key]
        public int UserChannelId { get; set; }

        // Foreign keys
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ChannelId { get; set; }

        [Required]
        public int RoleId { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Channel Channel { get; set; }
        public Role Role { get; set; }

        // New TimeSpan
        public TimeSpan TimeInChannel { get; set; }
    }
}