using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace PlanningSystem.Models {
    public class Role {
        public int roleId { get; set; }

        [DisplayName("Rol")]
        public string roleName { get; set; }

        public IEnumerable<SelectListItem> allRoles { get; set; }
    }
}