using Microsoft.EntityFrameworkCore;
using Server.Models;


namespace Server.Data
{
    public class UserManagerDB : DbContext
    {
        public UserManagerDB(DbContextOptions<UserManagerDB> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<SessionStore> SesssionStore { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Settings)
                .WithOne(s => s.User)
                .HasForeignKey<Settings>(s => s.UserId);
        }
    }
}