using System.Collections.Generic;

namespace University.Models
{
  public class Department
  {
    public int DeptId { get; set; }
    public string DeptName { get; set; }
    public List<Course> Courses { get; set; }
    public List<Student> Students { get; set; }
  }
}