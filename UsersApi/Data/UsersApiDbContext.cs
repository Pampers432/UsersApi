using Microsoft.EntityFrameworkCore;
using UsersApi.Models;

namespace UsersApi.Data
{
    public class UsersApiDbContext : DbContext
    {
        public DbSet<User> users { get; set; } = null!;
        public DbSet<UserRole> userRoles { get; set; } = null!;
        public DbSet<UserToken> userTokens { get; set; } = null!;

        public UsersApiDbContext(DbContextOptions<UsersApiDbContext> options) : base(options)
        { }        
    }
}
