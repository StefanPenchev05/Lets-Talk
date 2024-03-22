using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Server.Models 
{
    public class SessionStore
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; }

        [Required]
        public byte[] Value { get; set; }

        [Required]
        public DateTime ExpiresAtTime { get; set; }

        public ulong? SlidingExpirationInSeconds { get; set; }

        public DateTime? AbsoluteExpiration { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}