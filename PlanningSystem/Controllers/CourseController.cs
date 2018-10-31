using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace PlanningSystem.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Overview()
        {
            var context = new PlanningSysteemEntities();
            List<Models.Course> allCourses = new List<Models.Course>();
            var courses = context.Course.ToList();

            foreach (Course i in courses)
            {
                Models.Course course = new Models.Course()
                {
                    courseId = i.courseId,
                    courseCode = i.courseCode,
                    courseName = i.courseName,
                    description = i.description,
                };
                allCourses.Add(course);
            }
            return View(allCourses);
        }

        // GET: PersonalDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        [HttpPost]

        public ActionResult CreateCourse(Models.Course course)
        {
            var context = new PlanningSysteemEntities();
            var newCourse = new Course
            {
                courseId = course.courseId,
                courseCode = course.courseCode,
                courseName = course.courseName,
                description = course.description
            };
            context.Course.Add(newCourse);
            context.SaveChanges();
            return RedirectToAction("Overview", "Course");
        }

        public ActionResult CreateCourse()
        {
            return RedirectToAction("Overview", "Course");
        }

    }
}