using Microsoft.EntityFrameworkCore;

namespace University.Models
{
  public class UniversityContext : DbContext
  {
      public DbSet<Course> Courses { get; set; }
      public DbSet<Student> Students { get; set; }
      public DbSet<CourseStudent> CourseStudents{get; set;}
    public UniversityContext(DbContextOptions options) : base(options)
    {

    }
  }
}