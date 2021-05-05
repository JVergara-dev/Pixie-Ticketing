using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pixie_Ticketing.Models;
using System.Web.Script.Serialization;
using Support;
namespace Pixie_Ticketing.Views.Home
{
    public class HomeController : Controller
    {
        JavaScriptSerializer jsserializer = new JavaScriptSerializer();
        // GET: Home
        [OutputCache(Duration = 0)]
        [Authorize]
        public ActionResult Index(string key = "")
        {
            if (!Request.IsAuthenticated)
            {
                return Redirect(Url.Action("Index","Login"));
            }
            HttpCookie authcookie = Request.Cookies[".PixieCookie"];
            Accounts.Account account = jsserializer.Deserialize<Accounts.Account>(Helpers.GetFormsAuthenticationCookie());

            HomeModel m = new HomeModel();

            return View(m);
        }
    }
}