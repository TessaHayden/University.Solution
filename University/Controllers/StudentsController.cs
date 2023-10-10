using Microsoft.AspNetCore.Mvc;
using University.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace University.Controllers
{
  public class StudentsController : Controller
  {
    private readonly UniversityContext _db;
    public StudentsController(UniversityContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<Student> model = _db.Students
                            .Include(student => student.Course)
                            .ToList();
      return View(model);
    }
    public ActionResult Create()
    {
      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "CourseName");
      return View();
    }
    [HttpPost]
    public ActionResult Create(Student student)
    {
      if (student.CourseId == 0)
      {
        return RedirectToAction("Create");
      }
      _db.Students.Add(student);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      Student thisStudent = _db.Students
        .Include(student => student.Course)
        .Include(student => student.JoinEntities)
        .FirstOrDefault(student => student.StudentId == id);
      return View(thisStudent);
    }
    public ActionResult Edit(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "CourseName");
      return View(thisStudent);
    }
    [HttpPost]
    public ActionResult Edit(Student student)
    {
      _db.Students.Update(student);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult AddDept(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(students => students.StudentId == id);
      ViewBag.DeptId = new SelectList(_db.Departments, "DeptId", "DeptName");
      return View(thisStudent);
    }
    [HttpPost]
    public ActionResult AddDept(Student student, int deptId)
    {
#nullable enable
      StudentDept? joinEntity = _db.StudentDept.FirstOrDefault(join => (join.DeptId == deptId && join.StudentId == student.StudentId));
#nullable disable
      if (joinEntity == null && deptId != 0)
      {
        _db.StudentDept.Add(new StudentDept() { DeptId = deptId, StudentId = student.StudentId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = student.StudentId });
    }
  }
}