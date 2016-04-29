using System.Data.Entity;
using WebLib.Models;

namespace WebLib.DependencyInjection
{
    public class TestAppContext : DbContext, IAppContext
    {
        public TestAppContext()
        {
            this.Cases = new TestCaseDbSet();
            this.Notifications = new TestNotificationDbSet();
        }

        public DbSet<Case> Cases { get; set; }
        public DbSet<Installation> Installations { get; }
        public DbSet<Position> Positions { get; }
        public DbSet<Notification> Notifications { get; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Case item) { }
        public void MarkAsModified(Installation item) { }
        public void MarkAsModified(Position item) { }
        public void MarkAsModified(Notification item) { }

        public void Dispose() { }
    }
}