using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanningSystem.Models
{
    public class Role
    {
        public int roleId { get; set; }
        public string roleName { get; set; }
        public List<string> allRoles { get; set; }
    }
}