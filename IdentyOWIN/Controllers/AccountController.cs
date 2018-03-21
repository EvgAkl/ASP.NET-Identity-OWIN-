using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentityOWIN.Models;
using IdentityOWIN.Infrastructure;

namespace IdentyOWIN.Controllers
{  
    [Authorize]
    public class AccountController : Controller
    {
        // private properties and methods
        private AppUserManager userManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private IAuthenticationManager authManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        //============================================================================
        // action methods

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] { "В доступе отказано" });
            }

            ViewBag.returnUrl = returnUrl;
            return View();
        }  // end Login() #1

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel details, string returnUrl)
        {
            if ((details.Name != null) && (details.Password != null))
            {
                AppUser user = await userManager.FindAsync(details.Name, details.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Некорректное имя или пароль");
                }
                else
                {
                    ClaimsIdentity ident = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

                    authManager.SignOut();
                    authManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, ident);
                    return Redirect(returnUrl);
                }
            }
            return View(details);
        } // end Login() #2

        [Authorize]
        public ActionResult Logout()
        {
            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }





    } // end class

} // end namespace