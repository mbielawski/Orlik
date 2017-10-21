using System.Web.Mvc;

namespace Orlik.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (IsUserSignedIn())
                return View();

            return RedirectToAction("SignIn", "Account");
        }

        public ActionResult Start()
        {
            if (!IsUserSignedIn())
                return View();

            return RedirectToAction("Index", "Home");
        }
    }
}