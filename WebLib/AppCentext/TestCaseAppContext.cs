using System.Data.Entity;
using WebLib.DbSet;
using WebLib.InterfaceAppContext;
using WebLib.Models;

namespace WebLib.AppCentext
{
    public class TestCaseAppContext : DbContext, ICaseAppContext
    {
        public TestCaseAppContext()
        {
            this.Cases = new TestCaseDbSet();
            this.Notifications = new TestNotificationDbSet();
        }

        public DbSet<Case> Cases { get; set; }
        public DbSet<Installation> Installations { get; }
        public DbSet<Position> Positions { get; }
        public DbSet<User> Users { get; }
        public DbSet<Notification> Notifications { get; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Case item) { }
        public void MarkAsModified(Installation item) { }
        public void MarkAsModified(Position item) { }
        public void MarkAsModified(User item) { }
        public void MarkAsModified(Notification item) { }

        public void Dispose() { }
    }
}