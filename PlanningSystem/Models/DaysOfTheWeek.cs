using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;

namespace PlanningSystem.Models
{
    public class DaysOfTheWeek
    {
        [DisplayName("Dag")]
        public string Day { get; set; }
        //public IEnumerable<SelectListItem> AllDays { get; set; }
    }
}