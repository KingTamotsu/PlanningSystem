using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Web.Mvc;

namespace PlanningSystem.Controllers {
    public class UnavailabilityController : Controller {
        public ActionResult Formulier() {
            ViewBag.Message = "Geef hier onbeschikbaarheid op.";

            return View();
        }

        public ViewResult Respons(DateTime date, string startTimeHour, string startTimeMinutes, string endTimeHour,
            string endTimeMinutes, string cause) {
            PlanningSysteemEntities context = new PlanningSysteemEntities();
            Unavailability newUnavailability = new Unavailability();
            newUnavailability.UnavailabilityCause = cause;
            newUnavailability.UnavailabilityStartTime = new DateTime(date.Year, date.Month, date.Day,
                Convert.ToInt16(startTimeHour), Convert.ToInt16(startTimeMinutes), 0);
            newUnavailability.UnavailabilityEndTime = new DateTime(date.Year, date.Month, date.Day,
                Convert.ToInt16(endTimeHour), Convert.ToInt16(endTimeMinutes), 0);
            newUnavailability.userID = 3;
            context.Unavailability.Add(newUnavailability);
            Models.Unavailability addedUnavailability = new Models.Unavailability {
                UnavailabilityStartTime = newUnavailability.UnavailabilityStartTime,
                UnavailabilityEndTime = newUnavailability.UnavailabilityEndTime,
                UnavailabilityCause = newUnavailability.UnavailabilityCause
            };
            try {
                context.SaveChanges();
                addedUnavailability.UnavailabilityCause = "Uploaden gelukt";
                return View(addedUnavailability);
            }
            catch (EntityException) {
                context.Unavailability.Remove(newUnavailability);
                addedUnavailability.UnavailabilityCause = "Uploaden gefaald";

                return View(addedUnavailability);
            }
        }

        public ViewResult Inzien() {
            PlanningSysteemEntities context = new PlanningSysteemEntities();
            DateTime startDay = new DateTime(2018, 9, 10, 9, 0, 0);
            int currentUserId;
            string currentUserName = "startwaarde";
            int week;
            if (Request.Form["Teachers"] == null)
            {
                currentUserId = 3;
            }
            else
            {
                currentUserId = Int32.Parse(Request.Form["Teachers"]);
            }
            if (Request.Form["Week"] == null)
            {
                week = 1;
            }
            else
            {
                week = Int32.Parse(Request.Form["Week"]);
            }

            DateTime monday = startDay.AddDays(7*(week-1));
            List<List<Models.Unavailability>> totalList = new List<List<Models.Unavailability>>();
            List<Models.Unavailability> mondayUnavailabilities = new List<Models.Unavailability>();
            totalList.Add(mondayUnavailabilities);
            List<Models.Unavailability> tuesdayUnavailabilities = new List<Models.Unavailability>();
            totalList.Add(tuesdayUnavailabilities);
            List<Models.Unavailability> wednesdayUnavailabilities = new List<Models.Unavailability>();
            totalList.Add(wednesdayUnavailabilities);
            List<Models.Unavailability> thursdayUnavailabilities = new List<Models.Unavailability>();
            totalList.Add(thursdayUnavailabilities);
            List<Models.Unavailability> fridayUnavailabilities = new List<Models.Unavailability>();
            totalList.Add(fridayUnavailabilities);
            int i = 0;
            foreach (List<Models.Unavailability> subday in totalList) {
                DateTime dayoftheweek = monday.AddDays(i);
                List<Unavailability> tempList = context.Unavailability.Where(a =>
                        a.UnavailabilityStartTime.Day == dayoftheweek.Day &&
                        a.UnavailabilityStartTime.Month == dayoftheweek.Month &&
                        a.UnavailabilityStartTime.Year == dayoftheweek.Year &&
                        a.userID == currentUserId)
                    .ToList();
                foreach (Unavailability u in tempList) {
                    Models.Unavailability unavailability = new Models.Unavailability {
                        UnavailabilityCause = u.UnavailabilityCause,
                        UnavailabilityStartTime = u.UnavailabilityStartTime,
                        UnavailabilityEndTime = u.UnavailabilityEndTime,
                        UnavailabilityID = u.UnavailabilityID,
                        userID = u.userID,
                    };
                    subday.Add(unavailability);
                }
                i++;
                subday.Sort((a, b) => a.UnavailabilityStartTime.CompareTo(b.UnavailabilityStartTime));
            }
            List<Account> AllUsers = context.Account.ToList();
            List<SelectListItem> teacherItems = new List<SelectListItem>();
            foreach (Account users in AllUsers)
            {
                teacherItems.Add(new SelectListItem
                {
                    Text = users.name,
                    Value = users.userId.ToString(),
                    Selected = users.userId == currentUserId ? true : false,
                });
            }
            foreach (SelectListItem item in teacherItems)
            {
                if (item.Value == currentUserId.ToString())
                {
                    currentUserName = item.Text;
                }

                
            }
            List<SelectListItem> weekItems = new List<SelectListItem>();
            for (int j = 1; j < 11; j++)
            {
                weekItems.Add(new SelectListItem
                {
                    Text = "Week: " + j,
                    Value = j.ToString(),
                    Selected = j == week ? true : false
                });
            }
            ViewData["ListItemsTeachers"] = teacherItems;
            ViewData["ListItemsWeek"] = weekItems;
            ViewData["currentWeek"] = "Week: " + week.ToString();
            ViewData["currentUser"] = "Beschikbaarheid van: " + currentUserName;
            return View(totalList);
        }
    }
}