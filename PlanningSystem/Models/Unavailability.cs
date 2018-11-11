using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;

namespace PlanningSystem.Models {
    public class Unavailability {
        [DisplayName("OnbeschikbaarheidId")]
        public int UnavailabilityID { get; set; }
        [DisplayName("Begintijd")]
        public DateTime UnavailabilityStartTime { get; set; }
        [DisplayName("Eindtijd")]
        public DateTime UnavailabilityEndTime { get; set; }
        [DisplayName("Reden")]
        public string UnavailabilityCause { get; set; }
        [DisplayName("Gebruikersnaam")]
        public int userID { get; set; }
    }
}