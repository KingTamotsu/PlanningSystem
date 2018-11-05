using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Net;
using System.Web.UI.WebControls;

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

        // GET: Course/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //POST: Course/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "courseId,courseCode,courseName,description")]Course course)
        {            
            try
            {
                var context = new PlanningSysteemEntities();
                if (ModelState.IsValid)
                {
                    context.Course.Add(course);
                    context.SaveChanges();
                    return RedirectToAction("Overview", "Course");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

            }
            return RedirectToAction("Overview", "Course");
        }
   
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Course/Create
        //[HttpPost]

        //public ActionResult CreateCourse(Models.Course course)
        //{
        //    var context = new PlanningSysteemEntities();
        //    var newCourse = new Course
        //    {
        //        courseId = course.courseId,
        //        courseCode = course.courseCode,
        //        courseName = course.courseName,
        //        description = course.description
        //    };
        //    context.Course.Add(newCourse);
        //    context.SaveChanges();
        //    return RedirectToAction("Overview", "Course");
        //}

        //public ActionResult CreateCourse()
        //{
        //    return RedirectToAction("Overview", "Course");
        //}

        // GET: Course/Edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var context = new PlanningSysteemEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = context.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        //POST: Course/Edit
        [HttpPost, ActionName("Edit")]
        public ActionResult EditCourse(int? id)
        {
            var context = new PlanningSysteemEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var courseToUpdate = context.Course.Find(id);
            if (TryUpdateModel(courseToUpdate, "", new string[] {"courseCode", "courseName", "description"}))
            {
                try
                {
                    context.SaveChanges();
                    return RedirectToAction("Overview", "Course");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(courseToUpdate);
        }



        //public ActionResult Edit()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult EditCourse()
        //{
        //    return RedirectToAction("Overview", "Course");
        //}

        //public ActionResult EditCourse(Models.Course course)
        //{
        //    var context = new PlanningSysteemEntities();
        //    if (context.Course.Any(c => c.courseId == course.courseId))
        //    {
        //        var edited = new Course()
        //        {
        //            courseId = course.courseId,
        //            courseCode = course.courseCode,
        //            courseName = course.courseName,
        //            description = course.description
        //        };

        //        context.SaveChanges();
        //        return RedirectToAction("Overview", "Course");
        //    }

        //    else
        //    {
        //        return RedirectToAction("Overview", "Course");
        //    }
        //}



        // GET: Course
        [HttpGet]
        public ActionResult disable()
        {
            return View();
        }

        [HttpPost]
        //POST: Course

        public ActionResult DisableCourse(int courseId)
        {
            var context = new PlanningSysteemEntities();

            try
            {
                Course course = context.Course.Find(courseId);
                context.Course.Remove(course);
                context.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Disable", "Course");
            }

            return RedirectToAction("Disable", "Course");
        }

        public ActionResult DisableCourse()
        {
            return View();
        }

        //public ActionResult DisableCourse(Models.Course course)
        //{
        //    var context = new PlanningSysteemEntities();
        //    if (context.Course.Any(c => c.courseId == course.courseId))
        //    {
        //        var disCourse = context.Course.Where(c => c.disable == false).FirstOrDefault();
        //        disCourse.disable = true;
        //    }

        //    context.SaveChanges();
        //    return RedirectToAction("Disable", "Course");
    }

    //GET: Course
 }