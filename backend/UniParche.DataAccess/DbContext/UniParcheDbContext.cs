using Microsoft.EntityFrameworkCore;
using UniParche.Domain.Entities;

namespace UniParche.DataAccess.DbContext;

public class UniParcheDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public UniParcheDbContext(DbContextOptions<UniParcheDbContext> options)
        : base(options) { }

    // ========== DbSets ==========
    public DbSet<User> Users { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventAttendee> EventAttendees { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupMember> GroupMembers { get; set; }
    public DbSet<Friendship> Friendships { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ═══ Universidad → Usuarios ═══
        modelBuilder.Entity<University>()
            .HasMany(u => u.Users)
            .WithOne(u => u.University)
            .HasForeignKey(u => u.UniversityId)
            .OnDelete(DeleteBehavior.Restrict);

        // ═══ Usuario → Posts ═══
        modelBuilder.Entity<User>()
            .HasMany<Post>()
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // ═══ Usuario → Comentarios ═══
        modelBuilder.Entity<User>()
            .HasMany<Comment>()
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        // ═══ Usuario → Likes ═══
        modelBuilder.Entity<User>()
            .HasMany<Like>()
            .WithOne(l => l.User)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        // ═══ Post → Comentarios ═══
        modelBuilder.Entity<Post>()
            .HasMany<Comment>()
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        // ═══ Post → Likes ═══
        modelBuilder.Entity<Post>()
            .HasMany<Like>()
            .WithOne(l => l.Post)
            .HasForeignKey(l => l.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        // ═══ Like → ReactionType ═══
        modelBuilder.Entity<Like>()
            .Property(l => l.ReactionType)
            .HasConversion<int>();

        // ═══ Event → Creator (User) ═══
        modelBuilder.Entity<Event>()
            .HasOne(e => e.Creator)
            .WithMany()
            .HasForeignKey(e => e.CreatorId)
            .OnDelete(DeleteBehavior.NoAction);

        // ═══ Event → University ═══
        modelBuilder.Entity<Event>()
            .HasOne(e => e.University)
            .WithMany()
            .HasForeignKey(e => e.UniversityId)
            .OnDelete(DeleteBehavior.Restrict);

        // ═══ EventAttendee → Event + User ═══
        modelBuilder.Entity<EventAttendee>()
            .HasOne(ea => ea.Event)
            .WithMany(e => e.EventAttendees)
            .HasForeignKey(ea => ea.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EventAttendee>()
            .HasOne(ea => ea.User)
            .WithMany()
            .HasForeignKey(ea => ea.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        // ═══ Group → Creator (User) ═══
        modelBuilder.Entity<Group>()
            .HasOne(g => g.Creator)
            .WithMany()
            .HasForeignKey(g => g.CreatorId)
            .OnDelete(DeleteBehavior.NoAction);

        // ═══ Group → University ═══
        modelBuilder.Entity<Group>()
            .HasOne(g => g.University)
            .WithMany()
            .HasForeignKey(g => g.UniversityId)
            .OnDelete(DeleteBehavior.Restrict);

        // ═══ GroupMember → Group + User ═══
        modelBuilder.Entity<GroupMember>()
            .HasOne(gm => gm.Group)
            .WithMany(g => g.Members)
            .HasForeignKey(gm => gm.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GroupMember>()
            .HasOne(gm => gm.User)
            .WithMany()
            .HasForeignKey(gm => gm.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        // ═══ Friendship → User1 + User2 ═══
        modelBuilder.Entity<Friendship>()
            .HasOne(f => f.User1)
            .WithMany()
            .HasForeignKey(f => f.User1Id)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Friendship>()
            .HasOne(f => f.User2)
            .WithMany()
            .HasForeignKey(f => f.User2Id)
            .OnDelete(DeleteBehavior.NoAction);

        // ═══ Índices ═══
        modelBuilder.Entity<Post>()
            .HasIndex(p => p.UserId).HasDatabaseName("IX_Post_UserId");
        modelBuilder.Entity<Post>()
            .HasIndex(p => p.CreatedAt).HasDatabaseName("IX_Post_CreatedAt");
        modelBuilder.Entity<Comment>()
            .HasIndex(c => c.PostId).HasDatabaseName("IX_Comment_PostId");
        modelBuilder.Entity<Comment>()
            .HasIndex(c => c.UserId).HasDatabaseName("IX_Comment_UserId");
        modelBuilder.Entity<Like>()
            .HasIndex(l => new { l.UserId, l.PostId })
            .HasDatabaseName("IX_Like_UserIdPostId").IsUnique();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email).HasDatabaseName("IX_User_Email").IsUnique();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName).HasDatabaseName("IX_User_UserName").IsUnique();
        modelBuilder.Entity<University>()
            .HasIndex(u => u.DomainEmail).HasDatabaseName("IX_University_DomainEmail").IsUnique();
        modelBuilder.Entity<EventAttendee>()
            .HasIndex(ea => new { ea.EventId, ea.UserId })
            .HasDatabaseName("IX_EventAttendee_EventId_UserId").IsUnique();
        modelBuilder.Entity<GroupMember>()
            .HasIndex(gm => new { gm.GroupId, gm.UserId })
            .HasDatabaseName("IX_GroupMember_GroupId_UserId").IsUnique();
        modelBuilder.Entity<Friendship>()
            .HasIndex(f => new { f.User1Id, f.User2Id })
            .HasDatabaseName("IX_Friendship_User1Id_User2Id").IsUnique();
    }
}