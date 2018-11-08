//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlanningSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class Schedule
    {
        public int ID { get; set; }
        public string SchoolYear { get; set; }
        public string Month { get; set; }
        public int SchoolWeekNumber { get; set; }
        public string Day { get; set; }
        public string ClassID { get; set; }
        public int userId { get; set; }
        public int courseId { get; set; }
        public string ClassroomID { get; set; }
        public Nullable<System.DateTime> DateStartTime { get; set; }
        public Nullable<System.DateTime> DateEndTime { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Classroom Classroom { get; set; }
        public virtual Course Course { get; set; }
        public virtual DaysOfTheWeek DaysOfTheWeek { get; set; }
        public virtual Months Months { get; set; }
        public virtual SchoolWeekNumber SchoolWeekNumber1 { get; set; }
        public virtual SchoolYears SchoolYears { get; set; }
        public virtual StudentClass StudentClass { get; set; }
    }
}
