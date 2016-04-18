using System.Linq;
using WebLib.Models;
using WebLib.TestDbSet;

namespace WebLib.DbSet
{
    public class TestPositionDbSet : TestDbSet<Position>
    {
        public override Position Find(params object[] keyValues)
        {
            return this.SingleOrDefault(position => position.Id == (long)keyValues.Single());
        }
    }
}