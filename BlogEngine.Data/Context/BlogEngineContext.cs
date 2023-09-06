using BlogEngine.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogEngine.Data.Context
{
	public class BlogEngineContext : DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=BlogEngine;User ID=sa;Password=Azuquitarpalcafe76_;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<PostStatus>().ToTable("PostStatus");

            modelBuilder.Entity<Comment>()
                .HasOne(e => e.User)
                .WithMany(e => e.Comments)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .HasOne(e => e.Post)
                .WithMany(e => e.Comments)
                .HasForeignKey(e => e.PostId)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .HasOne(e => e.User)
                .WithMany(e => e.Posts)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.Post)
                .HasForeignKey(e => e.CommentId)
                .IsRequired(false);

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, UserName = "NubeM", Name = "Nube", LastName = "Mejia", Email = "nubem@gmail.com", RoleId = 1 },
                new User { UserId = 2, UserName = "JuanD", Name = "Juan", LastName = "Ojeda", Email = "Juand@gmail.com", RoleId = 2 },
                new User { UserId = 3, UserName = "RayoC", Name = "Rayuela", LastName = "Castro", Email = "rayitoc@gmail.com", RoleId = 3 }) ;

            modelBuilder.Entity<User>()
                .HasOne(e => e.Role)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Posts)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.PostId);

            modelBuilder.Entity<Role>().HasData(
               new Role { RoleId = 1, Name = "Public" },
               new Role { RoleId = 2, Name = "Writer" },
               new Role { RoleId = 3, Name = "Editor" });

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<PostStatus>().HasData(
               new PostStatus { PostStatusId = 1, Name = "Created" },
               new PostStatus { PostStatusId = 2, Name = "Pending" },
               new PostStatus { PostStatusId = 3, Name = "Published" });

            modelBuilder.Entity<PostStatus>()
               .HasMany(e => e.Post)
               .WithOne(e => e.PostStatus)
               .HasForeignKey(e => e.PostId)
               .IsRequired();

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PostStatus> PostStatuses { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}

