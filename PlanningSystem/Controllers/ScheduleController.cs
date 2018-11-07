using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlanningSystem.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: Schedule
        public ActionResult CreateClassroom()
        {
            //PlanningSysteemEntities context = new PlanningSysteemEntities();
            //List<Models.Schedule> CreateSchedule = new List<Models.Schedule>();
            //List<Schedule> schedule = context.Account.Where(a => a.isDisabled == false).ToList(); // list classnaam listnaam voorwaarde en ophalen

            //var context = new PlanningSysteemEntities();
            //List<Models.StudentClass> AllClasses = new List<Models.StudentClass>();
            //var studentclasses = context.StudentClass.ToList();

            //foreach (StudentClass i in studentclasses)
            //{
                //Models.StudentClass studentclass = new Models.StudentClass()
                //{
                    //ClassID = i.ClassID
                //};
                //AllClasses.Add(studentclass);
            //}


            var context = new PlanningSysteemEntities();
            List<StudentClass> AllClasses = context.StudentClass.ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var Classes in AllClasses)
            {
                items.Add(new SelectListItem
                {
                    Text = Classes.ClassID,
                    Value = Classes.ClassID
                });
            }
            ViewData["ListItemsStudentClasses"] = items;
            return View();


            //return View(AllClasses);
        }
        [HttpPost]
        // POST: Account/Create
        public ActionResult CreateScheduleClassroom(Models.Schedule account)
        {
            var context = new PlanningSysteemEntities();
            var newSchedule = new Schedule
            {
                //userId = account.userId,
                //username = account.username,
                //password = account.password,
                ClassID = (Request.Form["StudentClass"]),
                //firstLogin = account.firstLogin = true,
                //isResetted = account.isResetted = false,
                //createdAt = account.createdAt = DateTime.Now,
                //isDisabled = account.isDisabled = false
            };
            context.Schedule.Add(newSchedule);
            context.SaveChanges();
            return RedirectToAction("Overview", "Account");
        }

    }
}