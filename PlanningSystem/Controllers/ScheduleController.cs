using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PlanningSystem.Controllers {
    public class ScheduleController : Controller {

        /// <summary>
        ///     These methods are for getting all the data needed to fill the drop down menu's to create schedules.
        /// </summary>
        /// <returns>View Page</returns>
        // GET: Schedule
        public ActionResult CreateSchedule() {
            PlanningSysteemEntities context = new PlanningSysteemEntities();
            List<StudentClass> AllClasses = context.StudentClass.ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (StudentClass Classes in AllClasses)
                items.Add(new SelectListItem {
                    Text = Classes.ClassID,
                    Value = Classes.ClassID
                });
            ViewData["ListItemsStudentClasses"] = items;

            List<SchoolYears> AllYears = context.SchoolYears.ToList();
            List<SelectListItem> items2 = new List<SelectListItem>();
            foreach (SchoolYears years in AllYears)
                items2.Add(new SelectListItem
                {
                    Text = years.SchoolYear,
                    Value = years.SchoolYear
                });
            ViewData["ListItemsYears"] = items2;

            List<Months> AllMonths = context.Months.ToList();
            List<SelectListItem> items3 = new List<SelectListItem>();
            foreach (Months months in AllMonths)
                items3.Add(new SelectListItem
                {
                    Text = months.Month,
                    Value = months.Month
                });
            ViewData["ListItemsMonths"] = items3;

            List<SchoolWeekNumber> AllWeeks = context.SchoolWeekNumber.ToList();
            List<SelectListItem> items4 = new List<SelectListItem>();
            foreach (SchoolWeekNumber weeks in AllWeeks)
                items4.Add(new SelectListItem
                {
                    Text = weeks.NumberOfTheWeek.ToString(),
                    Value = weeks.NumberOfTheWeek.ToString()
                });
            ViewData["ListItemsWeeks"] = items4;

            List<DaysOfTheWeek> AllDays = context.DaysOfTheWeek.ToList();
            List<SelectListItem> items5 = new List<SelectListItem>();
            foreach (DaysOfTheWeek days in AllDays)
                items5.Add(new SelectListItem
                {
                    Text = days.Day,
                    Value = days.Day
                });
            ViewData["ListItemsDays"] = items5;

            List<Account> AllUsers = context.Account.ToList();
            List<SelectListItem> items6 = new List<SelectListItem>();
            foreach (Account users in AllUsers)
                items6.Add(new SelectListItem
                {
                    Text = users.name,
                    Value = users.name
                });
            ViewData["ListItemsTeachers"] = items6;

            List<Course> AllCourses = context.Course.ToList();
            List<SelectListItem> items7 = new List<SelectListItem>();
            foreach (Course courses in AllCourses)
                items7.Add(new SelectListItem
                {
                    Text = courses.courseName,
                    Value = courses.courseName
                });
            ViewData["ListItemsCourses"] = items7;

            List<Classroom> AllClassrooms = context.Classroom.ToList();
            List<SelectListItem> items8 = new List<SelectListItem>();
            foreach (Classroom classrooms in AllClassrooms)
                items8.Add(new SelectListItem
                {
                    Text = classrooms.ClassroomID,
                    Value = classrooms.ClassroomID
                });
            ViewData["ListItemsClassrooms"] = items8;

            return View();
        }

        /// <summary>
        ///     This method opens the view page.
        /// </summary>
        /// <returns>View Page</returns>
        public ActionResult ScheduleConfirmed()
        {
            return View();
        }

        /// <summary>
        ///     This method is for creating a single schedule.
        /// </summary>
        /// <returns>Redirect to schedule confirmed page of schedule</returns>
        [HttpPost]
        // POST: Account/Create
        public ActionResult CreateScheduleClassroom(Models.Schedule sched) {
            PlanningSysteemEntities context = new PlanningSysteemEntities();
            Schedule newSchedule = new Schedule {
                SchoolYear = Request.Form["StudentYear"],
                Month = Request.Form["StudentMonth"],
                SchoolWeekNumber = int.Parse(Request.Form["StudentWeek"]),
                Day = Request.Form["StudentDay"],
                Teacher = (Request.Form["Teachers"]),
                courseName = (Request.Form["Courses"]),
                ClassroomID = Request.Form["Classrooms"],
                ClassID = Request.Form["StudentClass"]
            };
            context.Schedule.Add(newSchedule);
            context.SaveChanges();
            return RedirectToAction("ScheduleConfirmed", "Schedule");
        }
    }
}