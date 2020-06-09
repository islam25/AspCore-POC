using Microsoft.EntityFrameworkCore;
using School.Core.Models;

namespace School.Infrastructure.Context
{
    public class SchoolContext: DbContext
    {
        public SchoolContext(DbContextOptions options)
            :base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Department> Departments { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("sqlConnection");
            }
        }
    }
}
