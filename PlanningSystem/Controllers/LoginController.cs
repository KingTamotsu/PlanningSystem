using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlanningSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult checkAccount(Account account) {
            PlanningSysteemEntities context = new PlanningSysteemEntities();
            if (context.Account.Any(a => a.username == account.username))
            {
                return RedirectToAction("Index", "Home");
            } else {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}