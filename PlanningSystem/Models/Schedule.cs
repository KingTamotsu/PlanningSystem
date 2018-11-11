using System;
using System.Collections.Generic;

namespace PlanningSystem.Models {
    public class Schedule {
        public int ID { get; set; }
        public DateTime DateStartTime { get; set; }
        public DateTime DateEndTime { get; set; }
        public StudentClass ClassName { get; set; }
        public SchoolYears year { get; set; }
        public Months month { get; set; }
        public SchoolWeekNumber schoolweek { get; set; }
        public DaysOfTheWeek day { get; set; }
        public Account user { get; set; }
        public Course course { get; set; }
        public Classroom classroomname { get; set; }
    }
}