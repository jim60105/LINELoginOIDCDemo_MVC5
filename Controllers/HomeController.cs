using Microsoft.Owin.Security;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace LINELoginOIDCDemo_MVC5.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            return View(userClaims);
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            if (Request.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.SignOut(
                    new AuthenticationProperties() { RedirectUri = WebConfigurationManager.AppSettings["OpenIDConnect:RedirectUri"], AllowRefresh = true },
                    HttpContext.GetOwinContext()
                               .Authentication.GetAuthenticationTypes()
                               .Select(o => o.AuthenticationType).ToArray());
            }
            return RedirectToAction("Index");
        }
    }
}