using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebLib.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            //DefaultAuthenticationTypes.ApplicationCookie
            // Add custom user claims here
            return userIdentity;
        }

        // 4 additional rows added to User in ASPNetUsers
        [DisplayName("Fornavn")]
        public string FirstName { get; set; }
        [DisplayName("Efternavn")]
        public string LastName { get; set; }
        [DisplayName("Email notifikationer")]
        [UIHint("_Notification")]
        public bool EmailNotification { get; set; }
        [DisplayName("SMS notifikationer")]
        [UIHint("_Notification")]
        public bool SMSNotification { get; set; }
    }
}