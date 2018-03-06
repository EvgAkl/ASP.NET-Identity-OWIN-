using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using IdentityOWIN.Infrastructure;
using IdentityOWIN.Models;
using System.Threading.Tasks;


namespace IdentyOWIN.Controllers
{
    public class AdminController : Controller
    {
        // private properties and methods
        private AppUserManager userManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); 
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        } // end AddErrorsFromResult()

        // ================================================================================
        // action methods
        public ActionResult Index()
        { 
            return View(userManager.Users);
        }

        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Name, Email = model.Email };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            } // end if
            return View(model);
        } // end Create() #2




    } // end class

} // end namespace