using System;
using System.Web.Mvc;

namespace Orlik.Web.Controllers
{
    public class BaseController : Controller
    {
        private void SetCookies(string key, string subkey, string value, int expiration)
        {
            Response.Cookies[key][subkey] = value;
            Response.Cookies[key].Expires = DateTime.Now.AddSeconds(expiration);
        }

        protected void SetSignedInUser(string username)
            => SetCookies("User", "Username", username, 3600);

        protected bool IsUserSignedIn()
            => Request.Cookies["User"] != null;

        protected void SignOutUser()
            => SetCookies("User", "Username", null, -1);

        protected string GetSignedInUsername()
        {
            if (!IsUserSignedIn())
                throw new Exception("User is not signed in.");

            return Request.Cookies["User"]["Username"];
        }
    }
}