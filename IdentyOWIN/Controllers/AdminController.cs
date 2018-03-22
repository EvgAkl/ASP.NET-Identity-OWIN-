using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using IdentityOWIN.Infrastructure;
using IdentityOWIN.Models;
using System.Threading.Tasks;


namespace IdentyOWIN.Controllers
{
    [Authorize(Roles = "Administrators")]
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
        } // end Create() #1

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

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Пользователь не найден" });
            }
        } // end Delete()


        public async Task<ActionResult> Edit(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        } // end Edit() #1

        [HttpPost]
        public async Task<ActionResult> Edit(string id, string userName, string email, string password)
        {
            AppUser user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.UserName = userName;
                user.Email = email;
                IdentityResult validEmail = await userManager.UserValidator.ValidateAsync(user);

                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;
                if (password != string.Empty)
                {
                    validPass = await userManager.PasswordValidator.ValidateAsync(password);

                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = userManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Введите пароль");
                }

                //if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                if (validEmail.Succeeded && password != string.Empty && validPass.Succeeded)
                {
                    IdentityResult result = await userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }

            } // end main if
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(user);

        } // end Edit() #2











    } // end class

} // end namespace