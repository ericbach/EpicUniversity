using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Grade>()
                .Property(p => p.Gpa)
                .HasColumnType("decimal(2,1)");

            modelBuilder.Entity<Student>()
                .Property(p => p.Gpa)
                .HasColumnType("decimal(2,1)");

            modelBuilder.Entity<Course>()
                .HasOne(c => c.CourseLab)
                .WithOne(i => i.Course)
                .HasForeignKey<CourseLab>(c => c.CourseId);
        }
    }
}
