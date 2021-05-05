using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Support;
using Pixie_Ticketing.Models;
namespace Pixie_Ticketing.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [OutputCache(Duration = 0)]
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            Response.Cookies.Clear();
            if (Request.IsAuthenticated)
            {
                return Redirect(Url.Action("Index", "Home"));
            }

            if (returnUrl != null)
            {
                TempData["returnUrl"] = returnUrl;
            }

            LoginModel lm = new LoginModel();

            if (TempData["LoginData"] == null)
            {
                lm = new LoginModel();
                lm.ReturnUrl = returnUrl;
            }
            else
            {
                lm = (LoginModel)TempData["LoginData"];
            }
            return View(lm);
        }

        [OutputCache(Duration = 0)]
        [AllowAnonymous]
        public ActionResult LoginUrl()
        {
            return View();
        }

        [OutputCache(Duration = 0)]
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel lm)
        {
            string returnUrl = "";
            if(TempData["returnUrl"] != null)
            {
                returnUrl = TempData["returnUrl"].ToString();
                if (returnUrl.ToLower().Contains("logout"))
                {
                    returnUrl = "Home";
                }
            }
            else
            {
                returnUrl = lm.ReturnUrl;
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Username and Password is required.");
                lm.ReturnUrl = returnUrl;
                return View(lm);
            }

            lm.Username = lm.Username.ToUpper().Trim();

            Accounts accnts = new Accounts();
            Accounts.Account accnt = new Accounts.Account();
            
            if(accnt.Username == null)
            {
                ModelState.AddModelError("", "Username does not exist.");
                lm.ReturnUrl = returnUrl;
                return View(lm);
            }

            return View();
        }
    }
}