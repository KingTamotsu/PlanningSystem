using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlanningSystem.Models;

namespace PlanningSystem.ViewModels
{
    public class NewAccountModel
    {
        public IEnumerable<Role> Roles { get; set; }
        public Account Account { get; set; }

    }
}