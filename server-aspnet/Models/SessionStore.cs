using System;
using System.ComponentModel.DataAnnotations;

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
    }
}