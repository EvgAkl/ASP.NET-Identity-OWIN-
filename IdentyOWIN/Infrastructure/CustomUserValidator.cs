using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;
using IdentityOWIN.Models;

namespace IdentityOWIN.Infrastructure
{
    public class CustomUserValidator : UserValidator<AppUser>
    {
        public CustomUserValidator(AppUserManager manager) : base(manager) { }

        public override async Task<IdentityResult> ValidateAsync(AppUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);

            if (!user.Email.ToLower().EndsWith("@mail.com"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Любой email-адрес, отличный от mail.com запрещён");
                result = new IdentityResult(errors);
            }
            return result;
        } // end ValidateAsync()

    } // end class

} // end namespace