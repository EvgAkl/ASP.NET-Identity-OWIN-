using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.ComponentModel.DataAnnotations;
using IdentityOWIN.Infrastructure;
using IdentityOWIN.Models;


namespace IdentyOWIN.Controllers
{
    public class RoleAdminController : Controller
    {
        // private properties and methods
        private AppUserManager userManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private AppRoleManager roleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }

        private void AddErrorFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        // ===========================================================================
        // action methods
        public ActionResult Index()
        {
            return View(roleManager.Roles);
        } // end Index()

        public ActionResult Create()
        {
            return View();
        } // end Create()

        [HttpPost]
        public async Task<ActionResult> Create([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new AppRole(name));

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorFromResult(result);
                }
            }
            return View(name);
        } // end Create()

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            AppRole role = await roleManager.FindByIdAsync(id);

            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            } // end main if
            else
            {
                return View("Error", new string[] { "Роль не найдена" });
            }
        } // end Delete()

    } // end controller

} // end 