using Microsoft.AspNet.Identity.EntityFramework;
using TC_03_DI.Models;

namespace TC_03_DI.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }


    }
}