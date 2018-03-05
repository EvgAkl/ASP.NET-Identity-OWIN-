using IdentityOWIN.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityOWIN.Infrastructure
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext() : base("name=IdentityDb") { }

        static AppIdentityDbContext()
        {
            Database.SetInitializer(new IdentityDbInit());
        }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }


    } // end class AppIdentityDbContext

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppIdentityDbContext>
    {
        public void PerformInitialSetup(AppIdentityDbContext context)
        {

        }

        protected override void Seed(AppIdentityDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

    } // end class IdentityDbInit

    
} // end namespace