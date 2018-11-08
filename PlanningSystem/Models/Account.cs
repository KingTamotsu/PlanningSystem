using System;
using System.ComponentModel;

namespace PlanningSystem.Models {
    public class Account {
        public int userId { get; set; }

        [DisplayName("Gebruikersnaam")]
        public string username { get; set; }

        [DisplayName("Wachtwoord")]
        public string password { get; set; }

        [DisplayName("Volledige naam")]
        public string name { get; set; }

        public Role role { get; set; }

        [DisplayName("Gecreert op")]
        public DateTime createdAt { get; set; }

        public bool firstLogin { get; set; }
        public bool isResetted { get; set; }
        public bool isDisabled { get; set; }
    }
}