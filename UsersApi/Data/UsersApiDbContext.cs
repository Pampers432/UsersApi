using Microsoft.EntityFrameworkCore;
using UsersApi.Models;

namespace UsersApi.Data
{
    // UsersApiDbContext.cs
    public class UsersApiDbContext : DbContext
    {
        public DbSet<User> users { get; set; } = null!;
        public DbSet<UserRole> userRoles { get; set; } = null!;
        public DbSet<UserToken> userTokens { get; set; } = null!;

        public UsersApiDbContext(DbContextOptions<UsersApiDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.UserRole_Id);
        }
    }
}
