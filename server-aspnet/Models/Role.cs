namespace Server.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }

        //Navigation property
        public ICollection<UserChannel> UserChannels { get; set; }
    }
}