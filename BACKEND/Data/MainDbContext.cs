using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class MainDbContext : IdentityDbContext<User>
    {
        public DbSet<User> User { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<SalesItem> SalesItems { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Content> Content { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Content>().UseTpcMappingStrategy();

            modelBuilder.Entity<Video>().ToTable("Videos");
            modelBuilder.Entity<Picture>().ToTable("Pictures");
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<SalesItem>().ToTable("SalesItems");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasMany(u => u.Contents)
                      .WithOne(c => c.Owner)
                      .HasForeignKey(c => c.OwnerId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(u => u.Comments)
                      .WithOne(c => c.Owner)
                      .HasForeignKey(c => c.PosterId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(u => u.LikedContents)
                .WithMany(c => c.LikedByUsers)
                        .UsingEntity(j => j.ToTable("UserLikedContents"));
                entity.HasMany(u => u.DislikedContents)
                .WithMany(c => c.DislikedByUsers)
                        .UsingEntity(j => j.ToTable("UserDislikedContents"));
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.Owner)
                      .WithMany(u => u.Comments)
                      .HasForeignKey(c => c.PosterId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(c => c.Contents)
                      .WithMany(cn => cn.Comments)
                      .HasForeignKey(c => c.ContentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            

            base.OnModelCreating(modelBuilder);
        }
    }
}
