using FirstBlog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FirstBlog.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PostLikeDislike>().HasKey(pl => new { pl.PostId, pl.UserId });

            modelBuilder.Entity<PostLikeDislike>()
                .HasOne<Post>(pl => pl.Post)
                .WithMany(l => l.PostLikes)
                .HasForeignKey(pl => pl.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostLikeDislike>()
                .HasOne<User>(pl => pl.User)
                .WithMany(l => l.PostLikes)
                .HasForeignKey(pl => pl.UserId);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<PostLikeDislike> PostLikeDislike { get; set; }
    }
}
