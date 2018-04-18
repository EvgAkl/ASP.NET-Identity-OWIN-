using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using IdentityOWIN.Models;
using IdentityOWIN.Infrastructure;


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
            dict.Add("В роли Administrators?", HttpContext.User.IsInRole("Administrators"));
            dict.Add("В роли Users?", HttpContext.User.IsInRole("Users"));
            dict.Add("В роли Employees?", HttpContext.User.IsInRole("Employees"));
            dict.Add("В роли DCStaff?", HttpContext.User.IsInRole("DCStaff"));

            return dict;
        } // end GetData

        private AppUserManager userManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private AppUser currentUser
        {
            get
            {
                return userManager.FindByName(HttpContext.User.Identity.Name);
            }
        }

        // ======================================================================
        // action methods
        [Authorize]
        public ActionResult Index()
        {
            return View(GetData("Index"));
        } // end Index()

        [Authorize(Roles = "Users")]
        public ActionResult OtherAction()
        {
            return View("Index", GetData("OtherAction"));
        } // end OtherAction()

        [Authorize]
        public ActionResult UserProps()
        {
            return View(currentUser);
        } // end UserProps() #1

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> UserProps(Cities city)
        {
            AppUser user = currentUser;
            user.City = city;
            user.SetCountryFromCity(city);

            await userManager.UpdateAsync(user);

            return View(user);
        } // end UserProps() #1

        [NonAction]
        public static string GetCityName<TEnum>(TEnum item) where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("Тип TEnum должен быть перечислением");
            }
            else
            {
                return item.GetType().GetMember(item.ToString()).First().GetCustomAttribute<DisplayAttribute>().Name;
            }
        } // end GetCityName()





    } // end controller

} // end namespace