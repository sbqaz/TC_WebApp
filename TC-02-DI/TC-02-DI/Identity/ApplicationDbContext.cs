using Microsoft.AspNet.Identity.EntityFramework;
using TC_02_DI.Models;

namespace TC_02_DI.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IAppplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
    }
}