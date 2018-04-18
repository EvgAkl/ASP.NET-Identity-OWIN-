using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace IdentyOWIN.Controllers
{
    public class ClaimsController : Controller
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

        [Authorize(Roles = "DCStaff")]
        public string OtherAction()
        {
            return "This is protected method";
        } // end OtherAction()



    } // end controller

} // end namespace