using System.Linq;
using WebLib.Models;

namespace WebLib.DependencyInjection
{
    public class TestCaseDbSet : TestDbSet<Case>
    {
        public override Case Find(params object[] keyValues)
        {
            return this.SingleOrDefault(Case => Case.Id == (long)keyValues.Single());
        }
    }

    public class TestInstallationDbSet : TestDbSet<Installation>
    {
        public override Installation Find(params object[] keyValues)
        {
            return this.SingleOrDefault(installation => installation.Id == (long)keyValues.Single());
        }
    }

    public class TestPositionDbSet : TestDbSet<Position>
    {
        public override Position Find(params object[] keyValues)
        {
            return this.SingleOrDefault(position => position.Id == (long)keyValues.Single());
        }
    }

    public class TestUserDbSet : TestDbSet<User>
    {
        public override User Find(params object[] keyValues)
        {
            return this.SingleOrDefault(user => user.Id == (string)keyValues.Single());
        }
    }

    public class TestNotificationDbSet : TestDbSet<Notification>
    {
        public override Notification Find(params object[] keyValues)
        {
            return this.SingleOrDefault(notification => notification.Id == (long)keyValues.Single());
        }
    }
}