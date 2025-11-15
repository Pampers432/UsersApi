using Microsoft.EntityFrameworkCore;
using UsersApi.Models;
using Core.Models;

namespace UsersApi.Data
{
    public class UsersApiDbContext : DbContext
    {
        public DbSet<User> users { get; set; } = null!;
        public DbSet<UserRole> userRoles { get; set; } = null!;
        public DbSet<UserToken> userTokens { get; set; } = null!;
        public DbSet<Test> Tests { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;

        public UsersApiDbContext(DbContextOptions<UsersApiDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.UserRole_Id);

            modelBuilder.Entity<UserToken>()
                .HasOne(ut => ut.User)
                .WithMany()
                .HasForeignKey(ut => ut.UserId);

            // Конфигурация Test
            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(45);
                entity.Property(e => e.Users_id).IsRequired();

                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(t => t.Users_id)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Конфигурация Room
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ExitKeyWord).IsRequired().HasMaxLength(45);
                entity.Property(e => e.Test_id).IsRequired();

                entity.HasOne<Test>()
                      .WithMany(t => t.Rooms)
                      .HasForeignKey(r => r.Test_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация Question
            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Text).IsRequired().HasMaxLength(45);
                entity.Property(e => e.ExitKeyLetter)
                      .IsRequired()
                      .HasConversion(
                          v => v.ToString(),  
                          v => v[0]           
                      )
                      .HasMaxLength(1);
                entity.Property(e => e.Room_id).IsRequired();

                entity.HasOne(q => q.Room)
                      .WithMany(r => r.Questions)
                      .HasForeignKey(q => q.Room_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}