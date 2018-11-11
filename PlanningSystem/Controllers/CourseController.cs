using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PlanningSystem.Controllers {
    public class CourseController : Controller {
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
                    Value = account.userId.ToString()
                });
            ViewData["ListItems"] = items;
            return View();
        }

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
                    Value = account.userId.ToString()
                });
            ViewData["ListItems"] = items;
            return View(course);
        }


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
            courseDB.teacher = Request.Form["Account"];
            context.SaveChanges();
                    
            return RedirectToAction("Overview", "Course");
        }

        // GET: Course
        [HttpGet]
        public ActionResult Delete(Models.Course course) {
            return View(course);
        }

        [HttpPost]
        //POST: Course
        public ActionResult DisableCourse(Models.Course course){
            if (course.courseId != null){
                PlanningSysteemEntities context = new PlanningSysteemEntities();
                Course courseDB = context.Course.Where(a => a.courseId == course.courseId).FirstOrDefault();
                courseDB.disable = true;
                context.SaveChanges();
                return RedirectToAction("Overview", "Course");
            }
            return RedirectToAction("Overview", "Course");
        }

        //public ActionResult LinkTeacher(Models.Course course)
        //{
        //    //var teacher = (from c in context.Course
        //        //    join a in context.Account
        //        //        on c.courseId equals a.userId
        //        //    where a.name == "teacher"
        //        //    select new
        //        //    {
        //        //        naam = a.name
        //        //    }).ToList();

        //    return RedirectToAction("Overview", "Course");
        //}
    }
}