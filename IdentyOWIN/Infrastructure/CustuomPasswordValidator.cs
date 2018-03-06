using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;


namespace IdentityOWIN.Infrastructure
{
    public class CustuomPasswordValidator : PasswordValidator
    {
        public override async Task<IdentityResult> ValidateAsync(string password)
        {
            IdentityResult result = await base.ValidateAsync(password);

            if (password.Contains("12345"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Пароль не должен содержать последовательности чисел");
                result = new IdentityResult(errors);
            }
            return result;
        } // end ValidateAsync()

    } // end class

} // end namespace