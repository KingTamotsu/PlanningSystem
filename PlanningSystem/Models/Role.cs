﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlanningSystem.Models
{
    public class Role
    {
        public int roleId { get; set; }
        public string roleName { get; set; }
        public IEnumerable<SelectListItem> allRoles { get; set; }
    }
}