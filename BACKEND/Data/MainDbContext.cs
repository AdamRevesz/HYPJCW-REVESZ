using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MainDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<SalesItem> SalesItems { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public MainDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.HasMany(u => u.Comments)
                .WithOne(c => c.User);
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.HasKey(v => v.ContentId);
            });

            modelBuilder.Entity<Content>(entity =>
            {
                entity.HasKey(c => c.ContentId);
                entity.HasMany(c => c.Comments)
                .WithOne(c => c.Content);
                entity.HasOne(c => c.Owner)
                .WithMany(c => c.Contents);
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.HasKey(p => p.ContentId);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.ContentId);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(c => c.CommentId);
            });

            modelBuilder.Entity<SalesItem>(entity =>
            {
                entity.HasKey(c => c.ContentId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
