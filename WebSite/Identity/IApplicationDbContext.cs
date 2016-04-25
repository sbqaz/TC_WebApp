using System.Data.Entity;
using WebLib.Models;

namespace WebSite.Identity
{
    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser.ToDo> ToDoes { get; set; }
    }
}