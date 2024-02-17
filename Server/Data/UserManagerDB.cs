using Microsoft.EntityFrameworkCore;


namespace Server.Data
{
    public class UserManagerDB : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=LetsTalk",
            new MySqlServerVersion(new Version(8, 3, 0)));
        }
    }
}