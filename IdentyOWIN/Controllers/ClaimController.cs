using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace IdentyOWIN.Controllers
{
    public class ClaimController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ClaimsIdentity ident = HttpContext.User.Identity as ClaimsIdentity;

            if (ident == null)
            {
                return View("Error", new string[] { "Нет доступных требований" });
            }
            else
            {
                return View(ident.Claims);
            }

        } // end Index()



    } // end controller

} // end namespace