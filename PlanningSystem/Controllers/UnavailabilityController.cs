using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PlanningSystem.Controllers
{
    public class UnavailabilityController : Controller
    {
        /// <summary>
        /// This loads the view in which a user can register an unavailability, which is a partial or whole day in which
        /// he/her is unavailable for any school related tasks
        /// </summary>
        /// <returns></returns>
        public ActionResult Formulier()
        {
            ViewBag.Message = "Geef hier onbeschikbaarheid op.";

            return View();
        }
        /// <summary>
        /// This processes the values that the user has declared and registers them into the database.
        /// It then loads a response view with either a conformation or an error message.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="startTimeHour"></param>
        /// <param name="startTimeMinutes"></param>
        /// <param name="endTimeHour"></param>
        /// <param name="endTimeMinutes"></param>
        /// <param name="cause"></param>
        /// <returns></returns>
        public ViewResult Respons(DateTime date, string startTimeHour, string startTimeMinutes, string endTimeHour, string endTimeMinutes, string cause)
        {
            var context = new PlanningSysteemEntities();
            var newUnavailability = new Unavailability();
            newUnavailability.UnavailabilityCause = cause;
            newUnavailability.UnavailabilityStartTime = new DateTime(date.Year, date.Month, date.Day, Convert.ToInt16(startTimeHour), Convert.ToInt16(startTimeMinutes), 0);
            newUnavailability.UnavailabilityEndTime = new DateTime(date.Year, date.Month, date.Day, Convert.ToInt16(endTimeHour), Convert.ToInt16(endTimeMinutes), 0);
            newUnavailability.userID = 3;
            context.Unavailability.Add(newUnavailability);
            var addedUnavailability = new Models.Unavailability
            {
                UnavailabilityStartTime = newUnavailability.UnavailabilityStartTime,
                UnavailabilityEndTime = newUnavailability.UnavailabilityEndTime,
                UnavailabilityCause = newUnavailability.UnavailabilityCause
            };
            try
            {
                context.SaveChanges();
                addedUnavailability.UnavailabilityCause = "Uploaden gelukt";
                return View(addedUnavailability);
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                context.Unavailability.Remove(newUnavailability);
                addedUnavailability.UnavailabilityCause = "Uploaden gefaald";

                return View(addedUnavailability);

            }
        }

        /// <summary>
        /// This loads the view containing a weekly overview of all unavailabilities of a user
        /// </summary>
        /// <param name="week"></param>
        /// <returns></returns>
        public ViewResult Inzien(int week = 1)
        {
            PlanningSysteemEntities context = new PlanningSysteemEntities();
            DateTime startDay = new DateTime(2018, 9, 10, 9, 0, 0);
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
            foreach (List<Models.Unavailability> subday in totalList)
            {
                DateTime dayoftheweek = monday.AddDays(i);
                List<Unavailability> tempList = context.Unavailability.Where(a =>
                    (a.UnavailabilityStartTime.Day == dayoftheweek.Day) &&
                    (a.UnavailabilityStartTime.Month == dayoftheweek.Month) &&
                    (a.UnavailabilityStartTime.Year == dayoftheweek.Year) &&
                    (a.userID == 3))
                    .ToList();
                foreach (Unavailability u in tempList)
                {
                    Models.Unavailability unavailability = new Models.Unavailability()
                    {
                        UnavailabilityCause = u.UnavailabilityCause,
                        UnavailabilityStartTime = u.UnavailabilityStartTime,
                        UnavailabilityEndTime = u.UnavailabilityEndTime,
                        UnavailabilityID = u.UnavailabilityID,
                        userID = 3,
                    };
                    subday.Add(unavailability);
                }
                i++;
                subday.Sort((a, b) => a.UnavailabilityStartTime.CompareTo(b.UnavailabilityStartTime));
            }

            return View(totalList);
        }
    }
}

