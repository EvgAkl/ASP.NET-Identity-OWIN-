using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityOWIN.Models
{
    public class AppUser : IdentityUser
    {

    } // end class AppUser


    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }

        public AppRole(string name) : base(name) { }

    } // end class AppRole




} // end namespace