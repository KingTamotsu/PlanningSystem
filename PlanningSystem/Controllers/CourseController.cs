using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateCourse()
        {
            return RedirectToAction("Overview", "Course");
        }
    }
}