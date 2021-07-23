using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Spanish_Spain.1252");

            modelBuilder.Entity<Relationship>(entity =>
            {
                entity.ToTable("relationships");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("created_at");

                entity.Property(e => e.FollowedId).HasColumnName("followed_id");

                entity.Property(e => e.FollowerId).HasColumnName("follower_id");

                entity.HasOne(d => d.Followed)
                    .WithMany(p => p.RelationshipFolloweds)
                    .HasForeignKey(d => d.FollowedId)
                    .HasConstraintName("followed_id");

                entity.HasOne(d => d.Follower)
                    .WithMany(p => p.RelationshipFollowers)
                    .HasForeignKey(d => d.FollowerId)
                    .HasConstraintName("follower_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("created_at");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(255)
                    .HasColumnName("lastname");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.Property(e => e.Verified).HasColumnName("verified");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
