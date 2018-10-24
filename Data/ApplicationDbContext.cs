using EACA_API.Models.AccountEntities.Tokens;
using EACA_API.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EACA_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApiUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RefreshToken>()
              .HasAlternateKey(c => c.UserId)
              .HasName("refreshToken_UserId");

            builder.Entity<RefreshToken>()
              .HasAlternateKey(c => c.Token)
              .HasName("refreshToken_Token");

            base.OnModelCreating(builder);
        }
    }
}
