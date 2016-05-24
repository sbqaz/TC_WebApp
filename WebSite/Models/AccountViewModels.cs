using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nuværende adgangskode")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Adgangskoden skal være mindst {2} karakterer lang.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Ny adgangskode")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekræft ny adgangskode")]
        [Compare("NewPassword", ErrorMessage = "Ny adgangskode og bekræft ny adgangskode stemmer ikke overens")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Adgangskode")]
        public string Password { get; set; }

        [Display(Name = "Tryk her for at gemme log-in oplysninger")]
        public bool RememberMe { get; set; }
    }
}
