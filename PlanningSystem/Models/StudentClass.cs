using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace PlanningSystem.Models {
    public class StudentClass {
        [DisplayName("Klasnaam")]
        public string ClassID { get; set; }

        public int NumberOfStudents { get; set; }
    }
}