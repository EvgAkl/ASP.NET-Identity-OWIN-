using System.Web;
using System.Web.Mvc;
using IdentityOWIN.Infrastructure;
using Microsoft.AspNet.Identity.Owin;

namespace IdentyOWIN.Controllers
{
    public class AdminController : Controller
    {
        private AppUserManager userManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); 
            }
        }

        public ActionResult Index()
        { 
            return View(userManager.Users);
        }
    } // end class

} // end namespace