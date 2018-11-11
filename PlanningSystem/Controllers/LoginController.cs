using System.Linq;
using System.Web.Mvc;
using Google.Authenticator;

namespace PlanningSystem.Controllers {
    public class LoginController : Controller {
        private const string key = "qaz123!@@)(*";


        public ActionResult Login() {
            return View();
        }

        /// <summary>
        /// This method gets checks the database if the account exists and forwards the result to the view.
        /// </summary>
        /// <param name="account">Account object</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(Account account) {
            PlanningSysteemEntities context = new PlanningSysteemEntities();
            bool status = false;


            if (context.Account.Any(a => a.username == account.username && a.password == account.password)) {
                status = true;
                Session["Username"] = account.username;

                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                string UserUniqueKey = account.username + key;
                Session["UserUniqueKey"] = UserUniqueKey;
                SetupCode setupInfo =
                    tfa.GenerateSetupCode("PlanningSysteem", account.username, UserUniqueKey, 300, 300);
                ViewBag.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                ViewBag.SetupCode = setupInfo.ManualEntryKey;
            }

            ViewBag.Status = status;
            return View();
        }

        /// <summary>
        /// This method checks in the google authenticator app of the code is valid.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Verify2FA() {
            string token = Request["passcode"];
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            string UserUniqueKey = Session["UserUniqueKey"].ToString();
            bool isValid = tfa.ValidateTwoFactorPIN(UserUniqueKey, token);
            if (isValid) {
                Session["IsValid2FA"] = true;
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}