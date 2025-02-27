using API.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasKey(r => r.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            // One-to-many relationship between User and Role (each User has one Role)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict); // Optionally specify delete behavior

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)  // Role can have multiple Users
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId);
        }
    }
}
