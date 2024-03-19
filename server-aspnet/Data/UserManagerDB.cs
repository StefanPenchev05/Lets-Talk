using Microsoft.EntityFrameworkCore;
using Server.Models;


namespace Server.Data
{
    public class UserManagerDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Settings> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=LetsTalk;uid=root;",
            new MySqlServerVersion(new Version(8, 3, 0)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Settings)
                .WithOne(s => s.User)
                .HasForeignKey<Settings>(s => s.UserId);
        }
    }
}