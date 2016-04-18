using System.Data.Entity;
using WebLib.DbSet;
using WebLib.InterfaceAppContext;
using WebLib.Models;

namespace WebLib.AppCentext
{
    public class TestInstalltionAppContext : DbContext, IInstallationAppContext
    {
        public TestInstalltionAppContext()
        {
            this.Installations = new TestInstallationDbSet();
        }

        public DbSet<Installation> Installations { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Installation item) { }

        public void Dispose() { }
    }
    
}