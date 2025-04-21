using System.ComponentModel.DataAnnotations.Schema;
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
        public DbSet<Chats> Chats { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Supports> Supports { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Enrollments> Enrollments { get; set; }
        public DbSet<UserProgress> UserProgress { get; set; }
        public DbSet<Plans> Plans { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        // Добавляем DbSet для UserCourse
        public DbSet<UserCourse> UserCourses { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.Entity<UserCourse>(entity =>
            {
                entity.HasKey(uc => new { uc.user_id, uc.course_id });

                entity.HasIndex(uc => new { uc.user_id, uc.course_id })
                      .IsUnique();

                entity.HasOne(uc => uc.Users)
                    .WithMany()
                    .HasForeignKey(uc => uc.user_id);
                
                entity.HasOne(uc => uc.Courses)
                    .WithMany()
                    .HasForeignKey(uc => uc.course_id);

                entity.HasOne(uc => uc.Payments)
                    .WithMany()
                    .HasForeignKey(uc => uc.pay_id);
            });
        }
    }
}
