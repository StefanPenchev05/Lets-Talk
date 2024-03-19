namespace Server.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int SettingsId { get; set; }
        public Settings Settings { get; set; }

        // Naviagtion properties
        public ICollection<Message> Messages { get; set; }
        public ICollection<UserChannel> UserChannels { get; set; }
    }
}