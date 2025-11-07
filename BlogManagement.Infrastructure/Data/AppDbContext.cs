using Microsoft.EntityFrameworkCore;
using BlogManagement.Core.Entities;

namespace BlogManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Comment> Comments => Set<Comment>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.Author).IsRequired();
                entity.HasMany(e => e.Comments)
                       .WithOne(c => c.Post!)
                       .HasForeignKey(c => c.PostId)
                       .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
