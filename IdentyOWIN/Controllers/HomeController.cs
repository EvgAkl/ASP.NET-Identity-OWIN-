using System.Collections.Generic;
using System.Web.Mvc;

namespace IdentyOWIN.Controllers
{
    public class HomeController : Controller
    {
        // private properties and methods
        private Dictionary<string, object> GetData(string actionName)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("Action", actionName);
            dict.Add("Пользователь", HttpContext.User.Identity.Name);
            dict.Add("Аунтифицирован?", HttpContext.User.Identity.IsAuthenticated);
            dict.Add("Тип аунтификации", HttpContext.User.Identity.AuthenticationType);
            dict.Add("В роли Users?", HttpContext.User.IsInRole("Users"));

            return dict;
        }
        // ======================================================================
        // action methods
        [Authorize]
        public ActionResult Index()
        {
            return View(GetData("Index"));
        }

        [Authorize(Roles = "Users")]
        public ActionResult OtherAction()
        {
            return View("Index", GetData("OtherAction"));
        }


    } // end controller

} // end namespace