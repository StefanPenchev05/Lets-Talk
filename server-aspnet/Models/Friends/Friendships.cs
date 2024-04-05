using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class Friendship
    {
        [Key]
        public int FriendshipId { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int FriendId { get; set; }
        public User Friend { get; set; }

    }
}