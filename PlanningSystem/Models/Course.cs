using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;

namespace PlanningSystem.Models {
    public class Course {
        public int courseId;

        [DisplayName("Cursus Code")]
        public string courseCode { get; set; }

        [DisplayName("Cursus naam")]
        public string courseName { get; set; }

        [DisplayName("Omschrijving")]
        public string description { get; set; }

        [DisplayName("Colleges")]
        public List<string> lectures { get; set; }
        [DisplayName("Docent")]
        public string teacher { get; set; }
        public bool isDisabled { get; set; }

        public IEnumerable<SelectListItem> AllCourses { get; set; }
    }
}