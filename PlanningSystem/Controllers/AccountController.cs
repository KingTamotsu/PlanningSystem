using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PlanningSystem.Controllers
{
    public class AccountController : Controller
    {
        #region ViewPages

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
                    name = i.name,
                    role = roleinDB,
                    createdAt = i.createdAt,
                    firstLogin = i.firstLogin
                };
                allAccounts.Add(account);
            }
            return View(allAccounts);
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

        public ActionResult Edit(Models.Account account)
        {
            var context = new PlanningSysteemEntities();

            List<Role> roles = context.Role.ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var role in roles)
            {
                items.Add(new SelectListItem
                {
                    Text = role.roleName,
                    Value = role.roleId.ToString(),          
                });
            }
            ViewData["ListItems"] = items;
            return View(account);
        }

        public ActionResult Delete(Models.Account account)
        {
            return View(account);
        }

        public ActionResult Reset()
        {
            return View();
        }

        public ActionResult resettedpassword(string password)
        {
            Models.Account account = new Models.Account();
            ViewData["password"] = password;
            return View(account);
        }

        #endregion

        #region ActionResults

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
                name = account.name,
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


        [HttpPost]
        public ActionResult EditAccount(Account account)
        {
            var context = new PlanningSysteemEntities();
            Account accountDB = context.Account.Where(a => a.userId == account.userId).FirstOrDefault();
            accountDB.username = account.username;
            accountDB.name = account.name;
            accountDB.roleId = Int32.Parse(Request.Form["Role"]);
            context.SaveChanges();
            return RedirectToAction("Overview", "Account");
        }

        [HttpPost]
        // POST: Account/Reset
        public ActionResult ResetAccount(Account currentAccount)
        {
            string newPassword;

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

        [HttpPost]
        public ActionResult DeleteAccount(Models.Account account)
        {

            if (account.userId != null)
            {
                var context = new PlanningSysteemEntities();
                Account accountDB = context.Account.Where(a => a.userId == account.userId).FirstOrDefault();
                accountDB.isDisabled = true;
                context.SaveChanges();
                return RedirectToAction("Overview", "Account");
            } 

            return RedirectToAction("Overview", "Account");
        }
    }

    #endregion
}