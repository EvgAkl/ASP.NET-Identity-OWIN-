using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.ComponentModel.DataAnnotations;
using IdentityOWIN.Infrastructure;
using IdentityOWIN.Models;
using System.Linq;
using System.Collections.Generic;


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
        } // end Create() #1

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
        } // end Create() #2

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

        public async Task<ActionResult> Edit(string id)
        {
            AppRole role = await roleManager.FindByIdAsync(id);

            string[] memberIDs = role.Users.Select(x => x.UserId).ToArray();

            IEnumerable<AppUser> members = userManager.Users.Where(x => memberIDs.Any(a => a == x.Id));

            IEnumerable<AppUser> nonMembers = userManager.Users.Except(members);

            return View(new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        } // end Edit() #1


        [HttpPost]
        public async Task<ActionResult> Edit(RoleModeficationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    result = await userManager.AddToRoleAsync(userId, model.RoleName);

                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }

                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    result = await userManager.RemoveFromRoleAsync(userId, model.RoleName);

                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }

                }

                return RedirectToAction("Index");
            } // end main if

            return View("Error", new string[] { "Роль не найдена" });

        } // end Edit() #2







    } // end controller

} // end 