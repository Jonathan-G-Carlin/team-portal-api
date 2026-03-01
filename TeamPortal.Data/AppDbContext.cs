using Microsoft.EntityFrameworkCore;
using TeamPortal.Data.Entities;

namespace TeamPortal.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {
    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the User entity
        modelBuilder.Entity<User>(entity => {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.FirstName)
                .HasMaxLength(100);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(100);
            entity.Property(e => e.FamilyName)
                .HasMaxLength(100);

            entity.Property(e => e.Email)
                .HasMaxLength(256);
            entity.HasIndex(e => e.Email)
                .IsUnique();
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(256);

            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValue("User");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);
            entity.Property(e => e.CreatedOnUtc)
                .HasDefaultValueSql("GETUTCDATE()");
        });
    }

}
