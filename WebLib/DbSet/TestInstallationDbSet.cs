using System.Linq;
using WebLib.Models;
using WebLib.TestDbSet;

namespace WebLib.DbSet
{
    public class TestInstallationDbSet : TestDbSet<Installation>
    {
        public override Installation Find(params object[] keyValues)
        {
            return this.SingleOrDefault(installation => installation.Id == (long)keyValues.Single());
        }
    }
}