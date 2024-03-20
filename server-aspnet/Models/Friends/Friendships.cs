using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class Friendship
    {
        [Key]
        public int FriendshipId { get; set; }

        [Required]
        public int User1Id { get; set; }
        public User User1 { get; set; }

        [Required]
        public int User2Id { get; set; }
        public User User2 { get; set; }

    }
}