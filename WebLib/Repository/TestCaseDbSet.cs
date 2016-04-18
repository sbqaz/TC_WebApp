using System;
using System.Linq;
using WebLib.Models;

namespace API.Tests.Unit.Controllers
{
    public class TestCaseDbSet : TestDbSet<Case>
    {
        public override Case Find(params object[] keyValues)
        {
            return this.SingleOrDefault(Case => Case.Id == (long)keyValues.Single());
        }
    }
}