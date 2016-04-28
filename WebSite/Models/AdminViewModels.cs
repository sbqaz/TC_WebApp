using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebSite.Models
{

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Adgangskoden {0} skal være mindst {2} karakterer langt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Adgangskode")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekræft adgangskode")]
        [Compare("Password", ErrorMessage = "Adgangskode og bekræftet adgangskode stemmer ikke overens")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Fornavn")]
        public string FirstName { get; set; }
        [Display(Name = "Efternavn")]
        public string LastName { get; set; }
        [Display(Name = "Telefon nummer")]
        public string PhoneNumber { get; set; }
    }

    public class EditViewModel
    {
        [Display(Name = "Fornavn")]
        public string FirstName { get; set; }
        [Display(Name = "Efternavn")]
        public string LastName { get; set; }
        [Display(Name = "Telefon nummer")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email notifikation")]
        public bool EmailNotification { get; set; }
        [Display(Name = "SMS notifikationer")]
        public bool SMSNotification { get; set; }
    }

    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Bruger type")]
        public string Name { get; set; }   
    }
}