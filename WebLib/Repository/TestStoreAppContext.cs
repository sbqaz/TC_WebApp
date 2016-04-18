using System.Data.Entity;
using StoreApp.Models;
using WebLib.Models;

namespace API.Tests.Unit.Controllers
{
    public class TestStoreAppContext : IStoreAppContext
    {
        public TestStoreAppContext()
        {
            this.Cases = new TestCaseDbSet();
        }

        public DbSet<Case> Cases { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Case item) { }
        public void Dispose() { }
    }
}