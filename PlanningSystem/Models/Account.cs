﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PlanningSystem.Models
{
    public class Account
    {
        public int userId;
        [DisplayName("Gebruikersnaam")]
        public string username { get; set; }
        [DisplayName("Wachtwoord")]
        public string password { get; set; }
        //[DisplayName("Rol")]
        //public Role role { get; set; }
        [DisplayName("Gecreert op")]
        public DateTime createdAt { get; set; }
        public bool firstLogin { get; set; }

        //public List<Account> allAccounts { get; set; }

    }
}