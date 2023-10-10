using System.Collections.Generic;

namespace University.Models
{
  public class Course
  {
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public int StudentId { get; set; }
    public string CourseNumber { get; set; }
    public List<Student> Students { get; set; }
  }
}
