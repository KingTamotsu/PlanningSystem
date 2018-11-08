using System;
using System.Collections.Generic;

namespace PlanningSystem.Models {
    public class Schedule {
        public int ID { get; set; }
        //public string Schoolyear { get; set; }
        //public string Month { get; set; }
        //public int SchoolweekNumber { get; set; }
        //public string Day { get; set; }
        //public string ClassID { get; set; }
        //public int userId { get; set; }
        public int courseId { get; set; }
        public string ClassroomID { get; set; }
        //public DateTime DayDate { get; set; }
        public DateTime DateStartTime { get; set; }
        public DateTime DateEndTime { get; set; }
        public StudentClass ClassName { get; set; }
        public SchoolYears year { get; set; }
        public Months month { get; set; }
        public SchoolWeekNumber schoolweek { get; set; }
        public DaysOfTheWeek day { get; set; }
        public Account user { get; set; }
        //public List<Schedule> AllClasses { get; set; }
    }
}