using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanningSystem.Models
{
    public class Unavailability
    {
        public DateTime UnavailabilityStartTime { get; set; }
        public DateTime UnavailabilityEndTime { get; set; }
        public string UnavailabilityCause { get; set; }
        public int userID { get; set; }
    }
}