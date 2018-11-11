using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Web.Mvc;

namespace PlanningSystem.Controllers {
    public class UnavailabilityController : Controller {
        /// <summary>
        /// This leads the user to the viewpage in which the user can register an unavailability (which is a partial 
        /// or whole day in which the user is unavailabaly for any school related work)
        /// </summary>
        /// <returns>Viewpage to registar an unavailability</returns>
        public ActionResult Formulier() {
            ViewBag.Message = "Geef hier onbeschikbaarheid op.";

            return View();
        }

        /// <summary>
        /// This method is called when an unavailability is registered. It receives the declared values, creates an object
        /// with those values and uploads this object to the database.
        /// </summary>
        /// <param name="date"></param> This is the date on which the unavailability occurs
        /// <param name="startTimeHour"></param> This is the hour at which the unavailability starts. It has min = 9 and max = 18
        /// <param name="startTimeMinutes"></param> This is the time in minutes  at which the unavailability starts. It is either 00 or 30
        /// <param name="endTimeHour"></param> This is the hour at which the unavailability ends. It has min = 9 and max = 18
        /// <param name="endTimeMinutes"></param> This is the time in minutes  at which the unavailability endss. It is either 00 or 30
        /// <param name="cause"></param> This is the cause for the unavailability, like a docters apointment. It is a string
        /// <returns>(On succesful upload:) returns viewpage with a confirmation and a button to register another unavailability
        /// (on unsuccesful upload) returns a viewpage with an error message and a button to retry the upload </returns>
        public ViewResult Respons(DateTime date, string startTimeHour, string startTimeMinutes, string endTimeHour,
            string endTimeMinutes, string cause) {
            PlanningSysteemEntities context = new PlanningSysteemEntities();
            Unavailability newUnavailability = new Unavailability(); // Initiation of unavailability to be uploaded
            newUnavailability.UnavailabilityCause = cause; // Sets the cause for the unavailability
            newUnavailability.UnavailabilityStartTime = new DateTime(date.Year, date.Month, date.Day,
                Convert.ToInt16(startTimeHour), Convert.ToInt16(startTimeMinutes), 0); // Combines the values of the given date with the given times
            newUnavailability.UnavailabilityEndTime = new DateTime(date.Year, date.Month, date.Day,
                Convert.ToInt16(endTimeHour), Convert.ToInt16(endTimeMinutes), 0); // Same as above comment
            newUnavailability.userID = 3; //userID is hardcoded until functionaliity to request the id of the logged in user is added
            context.Unavailability.Add(newUnavailability);
            Models.Unavailability addedUnavailability = new Models.Unavailability {
                UnavailabilityStartTime = newUnavailability.UnavailabilityStartTime,
                UnavailabilityEndTime = newUnavailability.UnavailabilityEndTime,
                UnavailabilityCause = newUnavailability.UnavailabilityCause
            };
            try { // Attempt to save the changes to the DB
                context.SaveChanges();
                addedUnavailability.UnavailabilityCause = "Uploaden gelukt"; // Conformation of upload, is only executed if the SaveChanges was succesful
                // is passed through UnavailabilityCause because it is a quick and easy way to pass a string
                return View(addedUnavailability);
            }
            catch (EntityException) {
                context.Unavailability.Remove(newUnavailability); // Removes the new availability, to prevent that when the upload is retried, it wont send multiple copies
                addedUnavailability.UnavailabilityCause = "Uploaden gefaald"; // Error message, see comment above 

                return View(addedUnavailability);
            }
        }

        /// <summary>
        /// Shows a view with a weekly schedule, in which the unavailabilities of the selected user are shown for a selected week
        /// </summary>
        /// <returns>A list containing five lists, one for each day of the week</returns>
        public ViewResult Inzien() {
            PlanningSysteemEntities context = new PlanningSysteemEntities();
            DateTime startDay = new DateTime(2018, 9, 10, 9, 0, 0); //Starting time of the quarterly "block"
            int currentUserId; //The id of the user whoose unavailabilities are shown
            string currentUserName = "startwaarde"; //Name of the user whoose unavailabilities are shown, has a default value to be changed later
            int week; //Number of week in the quarterly "block"
            if (Request.Form["Teachers"] == null) //This occurs when the view is started for the first time, since it hasn't had values yet to pass
            {
                currentUserId = 3; //To be changed to the user that is currently logged in, functionality is not yet available
            }
            else
            {
                currentUserId = Int32.Parse(Request.Form["Teachers"]); //Result of the drop down menu to select a user whose unavailabilities are to be shown
            }
            if (Request.Form["Week"] == null) //Occurs when the view is started for the first time, default value is the first week
            {
                week = 1;
            }
            else
            {
                week = Int32.Parse(Request.Form["Week"]); //Passes the value of the selected week
            }

            DateTime monday = startDay.AddDays(7*(week-1)); //initiates the starting day of the selected week, which is the monday
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
            // totalList now contains five lists, one for each day of the week
            int i = 0;
            foreach (List<Models.Unavailability> subday in totalList) { //Goes through every day of the week
                DateTime dayoftheweek = monday.AddDays(i); // Changes the day of which the unavailabilities are filtered.
                // Starting value = 0, so it starts with no change from the current day, which is the monday
                List<Unavailability> tempList = context.Unavailability.Where(a =>
                        a.UnavailabilityStartTime.Day == dayoftheweek.Day &&
                        a.UnavailabilityStartTime.Month == dayoftheweek.Month &&
                        a.UnavailabilityStartTime.Year == dayoftheweek.Year &&
                        a.userID == currentUserId)
                    .ToList(); //Filters the DB with the selected values for time (day, month and year) and user (userID)
                foreach (Unavailability u in tempList) {
                    Models.Unavailability unavailability = new Models.Unavailability {
                        UnavailabilityCause = u.UnavailabilityCause,
                        UnavailabilityStartTime = u.UnavailabilityStartTime,
                        UnavailabilityEndTime = u.UnavailabilityEndTime,
                        UnavailabilityID = u.UnavailabilityID,
                        userID = u.userID,
                    };
                    subday.Add(unavailability); // Adds the found unavailabilities to the List
                }
                i++;
                subday.Sort((a, b) => a.UnavailabilityStartTime.CompareTo(b.UnavailabilityStartTime)); // Sort the unavailabilities
                //by starting time, so that the view can build the schedule in correct chronological order
            }
            List<Account> AllUsers = context.Account.ToList(); // Gets all accounts
            List<SelectListItem> teacherItems = new List<SelectListItem>();
            foreach (Account users in AllUsers) // Adds account info to an item list, to be passed to the view
            {
                teacherItems.Add(new SelectListItem
                {
                    Text = users.name,
                    Value = users.userId.ToString(),
                    Selected = users.userId == currentUserId ? true : false, // Sets last selected user as starting value for its drop down menu
                });
            }
            foreach (SelectListItem item in teacherItems)
            {
                if (item.Value == currentUserId.ToString())
                {
                    currentUserName = item.Text; // Sets the name of the user whose unavailabilities are shown, to be passed to the view later
                }
            }
            List<SelectListItem> weekItems = new List<SelectListItem>();
            for (int j = 1; j < 11; j++) // Fills the drop down menu for week selection
            {
                weekItems.Add(new SelectListItem
                {
                    Text = "Week: " + j,
                    Value = j.ToString(),
                    Selected = j == week ? true : false // Sets last selected week as starting vaulue for its drop down menu
                });
            }
            ViewData["ListItemsTeachers"] = teacherItems; // Values for the drop down manu to select a user
            ViewData["ListItemsWeek"] = weekItems; // Values for the drop down menu to select a week
            ViewData["currentWeek"] = "Week: " + week.ToString(); // Shows the week of the schedule that is currently shown
            ViewData["currentUser"] = "Beschikbaarheid van: " + currentUserName; // Shows the name of the user whose schedule is currently shown
            return View(totalList); // Passes the values of the unavalabilities, seperated by days
        }
    }
}