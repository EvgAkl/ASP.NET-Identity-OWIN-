using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IdentityOWIN.Models;

namespace IdentityOWIN.Infrastructure
{
    public class CustomUserValidator : IIdentityValidator<AppUser>
    {
        public async Task<IdentityResult> ValidateAsync(AppUser user)
        {
            List<string> errors = new List<string>();

            if (String.IsNullOrEmpty(user.UserName.Trim()))
            {
                errors.Add("Вы указали пустое имя.");
            }

            string userNamePattern = @"[a-zA-Z0-9a-яА-Я]+$";

            if (!Regex.IsMatch(user.UserName, userNamePattern))
            {
                errors.Add("В имени разрешается указывать буквы английского или русского языка и цифры");
            }

            if (errors.Count > 0)
            {
                return IdentityResult.Failed(errors.ToArray());
            }

            return IdentityResult.Success;
        } // end ValidateAsync()

    } // end class

} // end namespace