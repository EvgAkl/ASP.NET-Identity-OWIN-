using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;


namespace IdentityOWIN.Models
{
    public enum Cities
    {
        [Display(Name = "Лондон")]
        LONDON,

        [Display(Name = "Париж")]
        PARIS,

        [Display(Name ="Москва")]
        MOSCOW
    }

    public class AppUser : IdentityUser
    {
        public Cities City { get; set; }
    } // end class AppUser


    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }

        public AppRole(string name) : base(name) { }

    } // end class AppRole




} // end namespace