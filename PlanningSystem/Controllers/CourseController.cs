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
            return View();
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