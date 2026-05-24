using Microsoft.EntityFrameworkCore;
using UniParche.Domain.Entities;

namespace UniParche.DataAccess.DbContext;

public class UniParcheDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public UniParcheDbContext(DbContextOptions<UniParcheDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ═══ Universidad → Usuarios (One-to-Many) ═══
        modelBuilder.Entity<University>()
            .HasMany(u => u.Users)
            .WithOne(u => u.University)
            .HasForeignKey(u => u.UniversityId)
            .OnDelete(DeleteBehavior.Restrict);

        // ═══ Usuario → Posts (One-to-Many) ═══
        modelBuilder.Entity<User>()
            .HasMany<Post>()
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // ═══ Usuario → Comentarios (One-to-Many) ═══
        modelBuilder.Entity<User>()
            .HasMany<Comment>()
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        // ═══ Usuario → Likes (One-to-Many) ═══
        modelBuilder.Entity<User>()
            .HasMany<Like>()
            .WithOne(l => l.User)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        // ═══ Post → Comentarios (One-to-Many) ═══
        modelBuilder.Entity<Post>()
            .HasMany<Comment>()
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        // ═══ Post → Likes (One-to-Many) ═══
        modelBuilder.Entity<Post>()
            .HasMany<Like>()
            .WithOne(l => l.Post)
            .HasForeignKey(l => l.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        // ═══ Índices para optimización ═══
        modelBuilder.Entity<Post>()
            .HasIndex(p => p.UserId)
            .HasDatabaseName("IX_Post_UserId");

        modelBuilder.Entity<Post>()
            .HasIndex(p => p.CreatedAt)
            .HasDatabaseName("IX_Post_CreatedAt");

        modelBuilder.Entity<Comment>()
            .HasIndex(c => c.PostId)
            .HasDatabaseName("IX_Comment_PostId");

        modelBuilder.Entity<Comment>()
            .HasIndex(c => c.UserId)
            .HasDatabaseName("IX_Comment_UserId");

        modelBuilder.Entity<Like>()
            .HasIndex(l => new { l.UserId, l.PostId })
            .HasDatabaseName("IX_Like_UserIdPostId")
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.email)
            .HasDatabaseName("IX_User_Email")
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.user_name)
            .HasDatabaseName("IX_User_UserName")
            .IsUnique();

        modelBuilder.Entity<University>()
            .HasIndex(u => u.DomainEmail)
            .HasDatabaseName("IX_University_DomainEmail")
            .IsUnique();
    }
}
