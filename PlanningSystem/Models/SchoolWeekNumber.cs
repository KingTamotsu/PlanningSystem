using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;

namespace PlanningSystem.Models
{
    public class SchoolWeekNumber
    {
        [DisplayName("WeekNr")]
        public int NumberOfTheWeek { get; set; }
        public IEnumerable<SelectListItem> AllWeeks { get; set; }
    }
}