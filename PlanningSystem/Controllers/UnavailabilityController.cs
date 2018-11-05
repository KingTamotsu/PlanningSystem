using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PlanningSystem.Controllers
{
    public class UnavailabilityController : Controller
    {
        public ActionResult Formulier()
        {
            ViewBag.Message = "Geef hier onbeschikbaarheid op.";

            return View();
        }
        public ViewResult Respons(DateTime date, string startTijdUur, string startTijdMinuten, string eindTijdUur, string eindTijdMinuten, string reden)
        {
            var context = new PlanningSysteemEntities();
            var newUnavailability = new Unavailability();
            newUnavailability.UnavailabilityCause = reden;
            newUnavailability.UnavailabilityStartTime = new DateTime(date.Year, date.Month, date.Day, Convert.ToInt16(startTijdUur), Convert.ToInt16(startTijdMinuten), 0);
            newUnavailability.UnavailabilityEndTime = new DateTime(date.Year, date.Month, date.Day, Convert.ToInt16(eindTijdUur), Convert.ToInt16(eindTijdMinuten), 0);
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
            catch (System.Data.Entity.Core.EntityException e)
            {
                context.Unavailability.Remove(newUnavailability);
                addedUnavailability.UnavailabilityCause = "Uploaden gefaald";

                return View(addedUnavailability); 

            }



        }
    }
}