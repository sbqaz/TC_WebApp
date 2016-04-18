using System.Data.Entity;
using WebLib.DbSet;
using WebLib.InterfaceAppContext;
using WebLib.Models;

namespace WebLib.AppCentext
{
    public class TestUserAppContext : DbContext, IUserAppContext
    {
        public TestUserAppContext()
        {
            this.Users = new TestUserDbSet();
        }

        public DbSet<User> Users { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(User item) { }

        public void Dispose() { }
    }
}