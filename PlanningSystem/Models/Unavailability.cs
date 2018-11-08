using System;

namespace PlanningSystem.Models {
    public class Unavailability {
        public int UnavailabilityID { get; set; }
        public DateTime UnavailabilityStartTime { get; set; }
        public DateTime UnavailabilityEndTime { get; set; }
        public string UnavailabilityCause { get; set; }
        public int userID { get; set; }
    }
}