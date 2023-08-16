using HW2Sem.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class Context: DbContext
{
    public DbSet<UserCoins> UserCoins { get; set; }
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Purchase> Purchases { get; set; } = null!;
    public DbSet<UserAuthor> UserAuthors { get; set; } = null!;

    public Context(DbContextOptions<Context> options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany<Purchase>(u => u.Purchases)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        modelBuilder.Entity<Purchase>()
            .HasOne(p => p.User)
            .WithMany(u => u.Purchases);
        modelBuilder.Entity<Comment>().HasKey(c=>c.Id);
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Posts)
            .WithOne(p=>p.Author);
        modelBuilder.Entity<Post>()
            .HasOne(p => p.Author)
            .WithMany(p => p.Posts);
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Comments)
            .WithOne(c=>c.Post);
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u=>u.Comments);
        modelBuilder.Entity<User>()
            .HasMany(u => u.Comments);
        modelBuilder.Entity<UserAuthor>().HasKey(ua=> new {ua.UserId,ua.AuthorId});
        modelBuilder.Entity<UserAuthor>()
            .HasOne(ua => ua.User)
            .WithMany(u => u.Authors)
            .HasForeignKey(ua => ua.UserId);
        modelBuilder.Entity<UserAuthor>()
            .HasOne(ua => ua.Author)
            .WithMany(a => a.Users)
            .HasForeignKey(ua=>ua.AuthorId);
        modelBuilder.Entity<UserCoins>()
            .HasOne<User>(u => u.User)
            .WithMany(u => u.UserCoins);
    }
}