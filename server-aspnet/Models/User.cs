using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public byte[] ProfilePicture { get; set; } = null;

        // Navigation properties
        public int SettingsId { get; set; }
        public Settings Settings { get; set; }

        public ICollection<Message> Messages { get; set; }
        public ICollection<UserChannel> UserChannels { get; set; }
        public ICollection<SessionStore> Sessions { get; set; }
        public ICollection<Friendship> Friendships { get; set; }
        public ICollection<FriendRequest> FriendRequests { get; set; }

        public ICollection<Friendship> User1Friendships { get; set; }
        public ICollection<Friendship> User2Friendships { get; set; }
    }
}