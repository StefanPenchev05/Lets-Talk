using Microsoft.EntityFrameworkCore;
using Server.Models;


namespace Server.Data
{
    public class UserManagerDB : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=LetsTalk;uid=root;",
            new MySqlServerVersion(new Version(8, 3, 0)));
        }
    }
}