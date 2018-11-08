using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;

namespace PlanningSystem.Models
{
    public class SchoolYears
    {
        [DisplayName("Schooljaar")]
        public string SchoolYear { get; set; }
        public IEnumerable<SelectListItem> AllYears { get; set; }
    }
}