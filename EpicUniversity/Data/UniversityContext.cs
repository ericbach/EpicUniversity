using EpicUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace EpicUniversity.Data
{
    public class UniversityContext : DbContext // == Unit of Work
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {
        }

        public DbSet<Grade> Grades{ get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseLab> CourseLabs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Use lazy loading with proxies - https://docs.microsoft.com/en-us/ef/core/querying/related-data/lazy
            //optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Grade>()
                .Property(p => p.Gpa)
                .HasColumnType("decimal(2,1)");

            // Setup one-to-one relationship
            modelBuilder.Entity<Course>()
                .HasOne(c => c.CourseLab)
                .WithOne(i => i.Course)
                .HasForeignKey<CourseLab>(c => c.CourseId);
        }
    }
}
