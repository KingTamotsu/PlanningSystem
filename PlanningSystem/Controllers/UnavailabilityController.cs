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

        public ViewResult Respons(DateTime date, string startTijdUur, string startTijdMinuten, string eindTijdUur,
            string eindTijdMinuten, string reden) {
            PlanningSysteemEntities context = new PlanningSysteemEntities();
            Unavailability newUnavailability = new Unavailability();
            newUnavailability.UnavailabilityCause = reden;
            newUnavailability.UnavailabilityStartTime = new DateTime(date.Year, date.Month, date.Day,
                Convert.ToInt16(startTijdUur), Convert.ToInt16(startTijdMinuten), 0);
            newUnavailability.UnavailabilityEndTime = new DateTime(date.Year, date.Month, date.Day,
                Convert.ToInt16(eindTijdUur), Convert.ToInt16(eindTijdMinuten), 0);
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
                        a.userID == 3)
                    .ToList();
                foreach (Unavailability u in tempList) {
                    Models.Unavailability unavailability = new Models.Unavailability {
                        UnavailabilityCause = u.UnavailabilityCause,
                        UnavailabilityStartTime = u.UnavailabilityStartTime,
                        UnavailabilityEndTime = u.UnavailabilityEndTime,
                        UnavailabilityID = u.UnavailabilityID,
                        userID = 3
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