using System.Linq;
using WebLib.Models;
using WebLib.TestDbSet;

namespace WebLib.DbSet
{
    public class TestUserDbSet : TestDbSet<User>
    {
        public override User Find(params object[] keyValues)
        {
            return this.SingleOrDefault(user => user.Id == (string)keyValues.Single());
        }
    }
}