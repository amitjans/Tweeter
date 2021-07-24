using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Tweeter.Models
{
    public partial class tweeterContext : DbContext
    {
        public tweeterContext()
        {
        }

        public tweeterContext(DbContextOptions<tweeterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Relationship> Relationships { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
