using System;
using System.Collections.Generic;

namespace PlanningSystem.Models {
    public class Schedule {
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
        public StudentClass ClassName { get; set; }
        public List<Schedule> AllClasses { get; set; }
    }
}