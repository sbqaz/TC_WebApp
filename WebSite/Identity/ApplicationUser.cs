using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebSite.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        // 3 additional rows added to User in ASPNetUsers
        public string WorkNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        // Not implemented
        public class ToDo
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public bool IsDone { get; set; }
            public virtual ApplicationUser User { get; set; }
        }
    }
}