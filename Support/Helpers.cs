using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Web.Script.Serialization;
namespace Support
{
    public class Helpers
    {
        public static string GetFormsAuthenticationCookie()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            return FormsAuthentication.Decrypt(cookie.Value).UserData;
        }

        public static string GetSessionValue()
        {
            
            JavaScriptSerializer jsserializer = new JavaScriptSerializer();
            Accounts.Account account = jsserializer.Deserialize<Accounts.Account>(GetFormsAuthenticationCookie());
            if (account == null) return "error_trying_session";
            return account.Name;
        }

    }
}
