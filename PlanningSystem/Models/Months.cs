using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;

namespace PlanningSystem.Models
{
    public class Months
    {
        [DisplayName("Maand")]
        public string Month { get; set; }
    }
}