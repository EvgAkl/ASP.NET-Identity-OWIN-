namespace IdentyOWIN.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using IdentityOWIN.Models;
    using IdentityOWIN.Infrastructure;


    internal sealed class Configuration : DbMigrationsConfiguration<IdentityOWIN.Infrastructure.AppIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "IdentityOWIN.Infrastructure.AppIdentityDbContext";
        }

        protected override void Seed(IdentityOWIN.Infrastructure.AppIdentityDbContext context)
        {
            AppUserManager userManager = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleManager = new AppRoleManager(new RoleStore<AppRole>(context));

            string roleName = "Administrators";
            string userName = "Admin";
            string password = "myPassword";
            string email = "admin@mail.ru";

            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new AppRole(roleName));
            }

            AppUser user = userManager.FindByName(userName);

            if (user == null)
            {
                userManager.Create(new AppUser { UserName = userName, Email = email }, password);
                user = userManager.FindByName(userName);
            }

            if (!userManager.IsInRole(user.Id, roleName))
            {
                userManager.AddToRole(user.Id, roleName);
                userManager.Update(user);
            }

            foreach (AppUser dbUser in userManager.Users)
            {
                dbUser.City = Cities.MOSCOW;
            }

            context.SaveChanges();


        } // end Seed()



    } // end class

} // end namespace
