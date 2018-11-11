using System.Linq;
using System.Web.Mvc;
using Google.Authenticator;

namespace PlanningSystem.Controllers {
    public class LoginController : Controller {
        private const string key = "qaz123!@@)(*";

        // GET: Login
        public ActionResult Login() {
            return View();
        }

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