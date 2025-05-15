using ListOk.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ListOk.Infrastructure.Persistent
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Board> Boards => Set<Board>();
        public DbSet<Column> Columns => Set<Column>();
        public DbSet<Card> Cards => Set<Card>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Card>()
                .Property(c => c.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Column>()
                .HasIndex(c => new { c.BoardId, c.Order });

            modelBuilder.Entity<Card>()
                .HasIndex(c => new { c.ColumnId, c.Order });

            modelBuilder.Entity<Board>()
                .HasMany(b => b.Columns)
                .WithOne(c => c.Board)
                .HasForeignKey(c => c.BoardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Column>()
                .HasMany(c => c.Cards)
                .WithOne(card => card.Column)
                .HasForeignKey(card => card.ColumnId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Board>()
                .HasOne(b => b.User)
                .WithMany(u => u.Boards)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}