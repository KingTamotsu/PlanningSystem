using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PlanningSystem.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Overview()
        {
            PlanningSysteemEntities context = new PlanningSysteemEntities();
            List<Models.Account> allAccounts = new List<Models.Account>();
            List<Account> accounts = context.Account.Where(a => a.isDisabled == false).ToList();

            foreach (Account i in accounts)
            {
                Models.Role roleinDB = new Models.Role()
                {
                    roleId = i.Role.roleId,
                    roleName = i.Role.roleName
                };
                Models.Account account = new Models.Account() {
                    userId = i.userId,
                    username = i.username,
                    role = roleinDB,
                    createdAt = i.createdAt,
                    firstLogin = i.firstLogin
                };
                allAccounts.Add(account);
            }
            return View(allAccounts);
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            var context = new PlanningSysteemEntities();

            List<Role> roles = context.Role.ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var role in roles)
            {
                items.Add(new SelectListItem
                {
                    Text = role.roleName,
                    Value = role.roleId.ToString()
                });
            }
            ViewData["ListItems"] = items;
            return View();
        }

        public ActionResult Reset()
        {
            return View();
        }


        [HttpPost]
        // POST: Account/Create
        public ActionResult CreateAccount(Models.Account account)
        {
            var context = new PlanningSysteemEntities();
            var newAccount = new Account
            {
                userId = account.userId,
                username = account.username,
                password = account.password,
                roleId = Int32.Parse(Request.Form["Role"]),
                firstLogin = account.firstLogin = true,
                isResetted = account.isResetted = false,
                createdAt = account.createdAt = DateTime.Now,
                isDisabled = account.isDisabled = false
            };
            context.Account.Add(newAccount);
            context.SaveChanges();
            return RedirectToAction("Overview", "Account");
        }

        // PUT: Account/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult EditAccount(Account account)
        {
            return RedirectToAction("Overview", "Account");
        }

        // DEL: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View("Overview");
        }

        [HttpPost]
        // POST: Account/Reset
        public ActionResult ResetAccount(Account currentAccount)
        {
            string newPassword; // = "testrggr";

            var context = new PlanningSysteemEntities();
            if (context.Account.Any(a => a.username == currentAccount.username))
            {
                const string Chars = "ABCDEFGHIJKLMNPOQRSTUVWXYZ0123456789";
                var random = new System.Random();
                var result = new string(
                    Enumerable.Repeat(Chars, 8)
                        .Select(s => s[random.Next(s.Length)])
                        .ToArray());
                newPassword = result;

                using (context)
                {
                    Account accountCurrent = context.Account.Where(a => a.username == currentAccount.username).FirstOrDefault();
                    accountCurrent.password = result;
                    accountCurrent.isResetted = true;
                    context.SaveChanges();
                }
                return RedirectToAction("resettedpassword", "Account", new { password = newPassword });
            }
            else {
                newPassword = "Deze gebruiker bestaat niet";
                return RedirectToAction("Reset", "Account");

            }
        }

        public ActionResult resettedpassword(string password)
        {
            Models.Account account = new Models.Account();
            ViewData["password"] = password;
            return View(account);
        }
    }
}