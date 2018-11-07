using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanningSystem.Models
{
    public class Schedule
    {
        public int ID;
        public string ClassID { get; set; }
        public int courseId { get; set; }
        public int userId { get; set; }
        public int SchoolweekNumber { get; set; }
        public string Schoolyear { get; set; }
        public DateTime DayDate { get; set; }
        public DateTime DateStartTime { get; set; }
        public DateTime DateEndTime { get; set; }
        public string ClassroomID { get; set; }
    }
}