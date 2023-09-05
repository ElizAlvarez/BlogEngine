using System;
using BlogEngine.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogEngine.Data.Context
{
	public class BlogEngineContext : DbContext
	{
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PostStatus> PostStatuses { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=BlogEngine;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(e => e.Role)
                .WithOne(e => e.User)
                .HasForeignKey<Role>(e => e.RoleId)
                .IsRequired();


            modelBuilder.Entity<Post>()
                .HasOne(e => e.User)
                .WithOne(e => e.Post)
                .HasForeignKey<User>(e => e.UserId)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .HasOne(e => e.PostStatus)
                .WithOne(e => e.Post)
                .HasForeignKey<PostStatus>(e => e.PostStatusId)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.Post)
                .HasForeignKey(e => e.CommentId)
                .IsRequired(false);

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, Name = "Public" },
                new Role { RoleId = 2, Name = "Writer" },
                new Role { RoleId = 3, Name = "Editor" });

            modelBuilder.Entity<PostStatus>().HasData(
                new PostStatus { PostStatusId = 1, Name = "Created" },
                new PostStatus { PostStatusId = 2, Name = "Pending" },
                new PostStatus { PostStatusId = 3, Name = "Published" });
        }
    }
}

