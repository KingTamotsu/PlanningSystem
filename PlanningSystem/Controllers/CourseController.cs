using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PlanningSystem.Controllers {
    public class CourseController : Controller {

        #region ViewPages

        /// <summary>
        ///     This method gets all courses in the database and forwards it to the view.
        /// </summary>
        /// <returns>View Page</returns>
        // GET: Course
        public ActionResult Overview() {
            PlanningSysteemEntities context = new PlanningSysteemEntities();
            List<Models.Course> allCourses = new List<Models.Course>();
            List<Course> courses = context.Course.Where(a => a.disable == false).ToList();
          
            foreach (Course i in courses) {
                Models.Course course = new Models.Course {
                    courseId = i.courseId,
                    courseCode = i.courseCode,
                    courseName = i.courseName,
                    description = i.description,
                    hoursPerWeek = i.hoursPerWeek,
                    teacher = i.teacher
                
                };
                allCourses.Add(course);
            }

            return View(allCourses);
        }

        /// <summary>
        ///     This method opens the view page to add a course.
        /// </summary>
        /// <returns>View Page</returns>
        /// 
        // GET: Course/Create
        [HttpGet]
        public ActionResult Create() {
            PlanningSysteemEntities context = new PlanningSysteemEntities();

            List<Account> accounts = context.Account.ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            //var teachers = context.Account.Select(c => new {text = c.name, Value = c.userId.ToString() }).ToList();
            foreach (Account account in accounts)
                items.Add(new SelectListItem()
                {
                    Text = account.name,
                    Value = account.name,
                    
        });
            ViewData["ListItems"] = items;
            return View();
        }

        /// <summary>
        ///     This method gets all courses from database and forwards it to the view.
        /// </summary>
        /// <param name="courses">Course object</param>
        /// <returns></returns>
        // GET: Course/Edit
        [HttpGet]
        public ActionResult Edit(Models.Course course)
        {
            PlanningSysteemEntities context = new PlanningSysteemEntities();
            List<SelectListItem> items = new List<SelectListItem>();
            List<Account> accounts = context.Account.ToList();
            foreach (Account account in accounts)
                items.Add(new SelectListItem()
                {
                    Text = account.name,
                    Value = account.name,
                });
            ViewData["ListItems"] = items;
            return View(course);
        }

        /// <summary>
        ///     This method opens the view page.
        /// </summary>
        /// <returns>View Page</returns>
        // GET: Course
        [HttpGet]
        public ActionResult Delete(Models.Course course)
        {
            return View(course);
        }

        #endregion

        #region ActionResults

        /// <summary>
        ///     This method creates an course.
        /// </summary>
        /// <param name="course">Course object</param>
        /// <returns>Redirect to Overview page of course.</returns>

        //POST: Course/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "courseId,courseCode,courseName,description,hoursPerWeek,teacher")]
            Course course) {
            try {
                PlanningSysteemEntities context = new PlanningSysteemEntities();
                if (ModelState.IsValid) {
                    context.Course.Add(course);
                    context.SaveChanges();
                    return RedirectToAction("Overview", "Course");
                }
                ViewData["ListItems"] = context;
            }
            catch (RetryLimitExceededException /* dex */) {
                ModelState.AddModelError("",
                    "Kan de wijzigingen niet opslaan, probeer opnieuw.");
            }

            return RedirectToAction("Overview", "Course");
        }

        /// <summary>
        ///     This method edits an course.
        /// </summary>
        /// <param name="course">Course object</param>
        /// <returns>Redirect to Overview page of course.</returns>

        //POST: Course/Edit
        [HttpPost]
        public ActionResult EditCourse(Models.Course course)
        {
            PlanningSysteemEntities context = new PlanningSysteemEntities();
            Course courseDB = context.Course.Where(a => a.courseId == course.courseId).FirstOrDefault();
            courseDB.courseCode = course.courseCode;
            courseDB.courseName = course.courseName;
            courseDB.description = course.description;
            courseDB.hoursPerWeek = course.hoursPerWeek;
            courseDB.teacher = Request.Form["teacher"];
            context.SaveChanges();
                    
            return RedirectToAction("Overview", "Course");
        }
        /// <summary>
        ///     THis method is to disable(soft delete) an course.
        /// </summary>
        /// <param name="course">Course object</param>
        /// <returns>Redirect to Overview Page</returns>
        [HttpPost]
        //POST: Course
        public ActionResult DisableCourse(Models.Course course)
        {
            if (course.courseId != null)
            {
                PlanningSysteemEntities context = new PlanningSysteemEntities();
                Course courseDB = context.Course.Where(a => a.courseId == course.courseId).FirstOrDefault();
                courseDB.disable = true;
                context.SaveChanges();
                return RedirectToAction("Overview", "Course");
            }
            return RedirectToAction("Overview", "Course");
        }
        #endregion
    }
}