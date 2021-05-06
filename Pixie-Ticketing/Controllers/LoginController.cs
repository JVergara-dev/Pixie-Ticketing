using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Support;
using Pixie_Ticketing.Models;
using System.Web.Script.Serialization;
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
            Accounts.Account accnt = accnts.SelectUserByUsername(lm.Username);
            
            if(accnt.Username == null)
            {
                ModelState.AddModelError("", "Username does not exist.");
                lm.ReturnUrl = returnUrl;
                return View(lm);
            }

            string hashing = PRMS.GetMD5Hash(lm.Password);

            if (PRMS.GetMD5Hash(lm.Password) != accnt.Password)
            {
                ModelState.AddModelError("", "Password is incorrect.");
                return View(lm);
            }
            else
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string data = serializer.Serialize(accnt);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, lm.Username, DateTime.Now, DateTime.Now.AddHours(8), true, data, FormsAuthentication.FormsCookiePath);
                string encriptedTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie ticketCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encriptedTicket);
                Accounts.Account x = serializer.Deserialize<Accounts.Account>(data);
                Response.Cookies.Add(ticketCookie);

                Parameters.UserID = accnt.ID;
                Parameters.UserName = accnt.Name;
                Parameters.UsedUsername = accnt.Username;
                Parameters.Password = accnt.Password;
                Parameters.Department = accnt.DepartmentID;
                Parameters.Role = accnt.RoleID;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}