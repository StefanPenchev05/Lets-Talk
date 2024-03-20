using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class FriendRequest
    {
         [Key]
        public int FriendRequestId { get; set; }

        [Required]
        public int RequesterId { get; set; }
        public User Requester { get; set; }

        [Required]
        public int RequestedId { get; set; }
        public User Requested { get; set; }
    }
}