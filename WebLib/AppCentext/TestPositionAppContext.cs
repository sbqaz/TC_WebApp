using System.Data.Entity;
using WebLib.DbSet;
using WebLib.InterfaceAppContext;
using WebLib.Models;

namespace WebLib.AppCentext
{
    public class TestPositionAppContext : DbContext, IPositionAppContext
    {
        public TestPositionAppContext()
        {
            this.Positions = new TestPositionDbSet();
        }

        public DbSet<Position> Positions { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Position item) { }

        public void Dispose() { }
    }
}