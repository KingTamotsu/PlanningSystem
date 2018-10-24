using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanningSystem.Models
{
    public class Course
    {
        public int courseId;
        public string courseCode;
        public string courseName;
        public string description;
        public List<string> lectures;
    }
}