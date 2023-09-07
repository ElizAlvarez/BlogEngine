using BlogEngine.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogEngine.Data.Context
{
	public class BlogEngineDbContext : DbContext
	{        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=[Database];User ID=[User];Password=[Password];TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<PostStatus>().ToTable("PostStatus");

            modelBuilder.Entity<Post>().HasKey(e => e.PostId);
            modelBuilder.Entity<User>().HasKey(e => e.UserId);
            modelBuilder.Entity<Comment>().HasKey(e => e.CommentId);
            modelBuilder.Entity<Role>().HasKey(e => e.RoleId);
            modelBuilder.Entity<PostStatus>().HasKey(e => e.PostStatusId);

            modelBuilder.Entity<Comment>()
                .Property(e => e.CommentId)
                .UseIdentityColumn(1, 1)
                .ValueGeneratedOnAdd();

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
                .Property(e => e.PostId)
                .UseIdentityColumn(1, 1)
                .ValueGeneratedOnAdd();

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

            modelBuilder.Entity<Post>()
                .HasOne(e => e.PostStatus)
                .WithMany(e => e.Posts)
                .HasForeignKey(e => e.PostStatusId)
                .IsRequired();

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, UserName = "NubeM", Name = "Nube", LastName = "Mejia", Email = "nubem@gmail.com", RoleId = 1 },
                new User { UserId = 2, UserName = "JuanD", Name = "Juan", LastName = "Ojeda", Email = "Juand@gmail.com", RoleId = 2 },
                new User { UserId = 3, UserName = "RayoC", Name = "Rayuela", LastName = "Castro", Email = "rayitoc@gmail.com", RoleId = 3 }) ;

            modelBuilder.Entity<User>()
               .Property(e => e.UserId)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .HasOne(e => e.Role)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Posts)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.PostId)
                .IsRequired(false);

            modelBuilder.Entity<Role>().HasData(
               new Role { RoleId = 1, Name = "Public" },
               new Role { RoleId = 2, Name = "Writer" },
               new Role { RoleId = 3, Name = "Editor" });

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.UserId)
                .IsRequired(false);

            modelBuilder.Entity<PostStatus>().HasData(
               new PostStatus { PostStatusId = 1, Name = "Created" },
               new PostStatus { PostStatusId = 2, Name = "Pending" },
               new PostStatus { PostStatusId = 3, Name = "Published" });

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PostStatus> PostStatuses { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}

