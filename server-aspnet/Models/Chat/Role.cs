using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [EnumDataType(typeof(RoleType))]
        public RoleType RoleType { get; set; }

        //Navigation property
        public ICollection<UserChannel> UserChannels { get; set; }
    }

    public enum RoleType
    {
        Admin,
        User,
        Spectator
    }
}