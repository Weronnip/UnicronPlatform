using Microsoft.EntityFrameworkCore;
using UnicronPlatform.Models;

namespace UnicronPlatform.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<Lessons> Lessons { get; set; }
        public DbSet<LessonLink> LessonLink { get; set; }
        public DbSet<Enrollments> Enrollments { get; set; }
        public DbSet<UserProgress> UserProgress { get; set; }
        public DbSet<Plans> Plans { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;",
                    ServerVersion.AutoDetect("server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;")
                );
            }
        }
    }
}