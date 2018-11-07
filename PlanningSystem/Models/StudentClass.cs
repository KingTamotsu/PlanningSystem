using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PlanningSystem.Models
{
    public class StudentClass
    {
        [DisplayName("klasnaam")]
        public string ClassID { get; set; }
        public int NumberOfStudents { get; set; }
        public IEnumerable<SelectListItem> AllClasses { get; set; }
    }
}