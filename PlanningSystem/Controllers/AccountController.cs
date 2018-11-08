using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PlanningSystem.Controllers {
    public class AccountController : Controller {
        #region ViewPages

        /// <summary>
        ///     This method gets all accounts in the database and forwards it to the view.
        /// </summary>
        /// <returns>View Page</returns>
        public ActionResult Overview() {
            PlanningSysteemEntities context = new PlanningSysteemEntities();
            List<Models.Account> allAccounts = new List<Models.Account>();
            List<Account> accounts = context.Account.Where(a => a.isDisabled == false).ToList();

            foreach (Account i in accounts) {
                Models.Role roleinDB = new Models.Role {
                    roleId = i.Role.roleId,
                    roleName = i.Role.roleName
                };
                Models.Account account = new Models.Account {
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

        /// <summary>
        ///     This method gets all roles from database and forwards it to the view.
        /// </summary>
        /// <returns>View Page</returns>
        public ActionResult Create() {
            PlanningSysteemEntities context = new PlanningSysteemEntities();

            List<Role> roles = context.Role.ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (Role role in roles)
                items.Add(new SelectListItem {
                    Text = role.roleName,
                    Value = role.roleId.ToString()
                });
            ViewData["ListItems"] = items;
            return View();
        }

        /// <summary>
        ///     This method gets all roles from database and forwards it to the view.
        /// </summary>
        /// <param name="account">Account object</param>
        /// <returns></returns>
        public ActionResult Edit(Models.Account account) {
            PlanningSysteemEntities context = new PlanningSysteemEntities();

            List<Role> roles = context.Role.ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (Role role in roles)
                items.Add(new SelectListItem {
                    Text = role.roleName,
                    Value = role.roleId.ToString()
                });
            ViewData["ListItems"] = items;
            return View(account);
        }

        /// <summary>
        ///     This method opens the view page.
        /// </summary>
        /// <param name="account">Account object</param>
        /// <returns>View Page</returns>
        public ActionResult Delete(Models.Account account) {
            return View(account);
        }

        /// <summary>
        ///     This method opens the view page.
        /// </summary>
        /// <returns>View Page</returns>
        public ActionResult Reset() {
            return View();
        }

        /// <summary>
        ///     This method opens the view page.
        /// </summary>
        /// <param name="password">String with the password</param>
        /// <returns>View Page</returns>
        public ActionResult resettedpassword(string password) {
            Models.Account account = new Models.Account();
            ViewData["password"] = password;
            return View(account);
        }

        #endregion

        #region ActionResults

        /// <summary>
        ///     This method creates an account.
        /// </summary>
        /// <param name="account">Account object</param>
        /// <returns>Redirect to Overview page of account.</returns>
        [HttpPost]
        public ActionResult CreateAccount(Models.Account account) {
            PlanningSysteemEntities context = new PlanningSysteemEntities();
            Account newAccount = new Account {
                userId = account.userId,
                username = account.username,
                password = account.password,
                name = account.name,
                roleId = int.Parse(Request.Form["Role"]),
                firstLogin = account.firstLogin = true,
                isResetted = account.isResetted = false,
                createdAt = account.createdAt = DateTime.Now,
                isDisabled = account.isDisabled = false
            };
            context.Account.Add(newAccount);
            context.SaveChanges();
            return RedirectToAction("Overview", "Account");
        }

        /// <summary>
        ///     This method update an account.
        /// </summary>
        /// <param name="account">Account object</param>
        /// <returns>Redirect to Overview page of account.</returns>
        [HttpPost]
        public ActionResult EditAccount(Account account) {
            PlanningSysteemEntities context = new PlanningSysteemEntities();
            Account accountDB = context.Account.Where(a => a.userId == account.userId).FirstOrDefault();
            accountDB.username = account.username;
            accountDB.name = account.name;
            accountDB.roleId = int.Parse(Request.Form["Role"]);
            context.SaveChanges();
            return RedirectToAction("Overview", "Account");
        }

        /// <summary>
        ///     This method is for resetting the password of a account.
        /// </summary>
        /// <param name="currentAccount">Account object</param>
        /// <returns>Redirect to Reset page of account or if it succeeds to ResettedPassword</returns>
        [HttpPost]
        public ActionResult ResetAccount(Account currentAccount) {
            string newPassword;

            PlanningSysteemEntities context = new PlanningSysteemEntities();
            if (context.Account.Any(a => a.username == currentAccount.username)) {
                const string Chars = "ABCDEFGHIJKLMNPOQRSTUVWXYZ0123456789";
                Random random = new Random();
                string result = new string(
                    Enumerable.Repeat(Chars, 8)
                        .Select(s => s[random.Next(s.Length)])
                        .ToArray());
                newPassword = result;

                using (context) {
                    Account accountCurrent = context.Account.Where(a => a.username == currentAccount.username)
                        .FirstOrDefault();
                    accountCurrent.password = result;
                    accountCurrent.isResetted = true;
                    context.SaveChanges();
                }

                return RedirectToAction("resettedpassword", "Account", new {password = newPassword});
            }

            newPassword = "Deze gebruiker bestaat niet";
            return RedirectToAction("Reset", "Account");
        }

        /// <summary>
        ///     THis method is to disable(soft delete) an account.
        /// </summary>
        /// <param name="account">Account object</param>
        /// <returns>Redirect to Overview Page</returns>
        [HttpPost]
        public ActionResult DeleteAccount(Models.Account account) {
            if (account.userId != 0) {
                PlanningSysteemEntities context = new PlanningSysteemEntities();
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