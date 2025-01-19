using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class MainDbContext : IdentityDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<SalesItem> SalesItems { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Comments> Comments { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> ctx) : base(ctx)
        {

        }

        public MainDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.HasMany(u => u.Contents)
                         .WithOne(c => c.Owner)
                         .HasForeignKey(c => c.OwnerId)
                         .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(u => u.Comments)
                        .WithOne(c => c.Poster)
                        .HasForeignKey(c => c.PosterId)
                        .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<Content>(entity =>
            {
                entity.HasKey(c => c.ContentId);
                entity.HasMany(c => c.Comments)
                         .WithOne(c => c.Contents)
                         .HasForeignKey(c => c.ContentId)
                         .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.Owner)
                         .WithMany(u => u.Contents)
                         .HasForeignKey(c => c.OwnerId)
                         .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(c => c.CommentId);

            });
            modelBuilder.Entity<Video>().HasBaseType<Content>();
            modelBuilder.Entity<Picture>().HasBaseType<Content>();
            modelBuilder.Entity<Course>().HasBaseType<Content>();
            modelBuilder.Entity<SalesItem>().HasBaseType<Content>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
