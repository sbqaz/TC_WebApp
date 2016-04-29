using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebLib.DependencyInjection;
using WebLib.Models;

namespace WebSite.Models
{
    public class AppContext : DbContext, IAppContext
    {
        public DbSet<Case> Cases { get;}
        public DbSet<Installation> Installations { get;}
        public DbSet<Position> Positions { get;}
        public DbSet<Notification> Notifications { get; }

        // Unit test methods
        public void MarkAsModified(Case item)
        {
            throw new NotImplementedException();
        }

        public void MarkAsModified(Installation item)
        {
            throw new NotImplementedException();
        }

        public void MarkAsModified(Position item)
        {
            throw new NotImplementedException();
        }

        public void MarkAsModified(Notification item)
        {
            throw new NotImplementedException();
        }
    }
}