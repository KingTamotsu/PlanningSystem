using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PlanningSystem.Models
{
    public class Role
    {
        public int roleId { get; set; }
        [DisplayName("Rol")]
        public string roleName { get; set; }
        public IEnumerable<SelectListItem> allRoles { get; set; }
    }
}