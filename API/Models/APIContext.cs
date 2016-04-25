using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebLib.DependencyInjection;
using WebLib.Models;

namespace API.Models
{
    public class ApiContext : DbContext, IAppContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ApiContext() : base("APIContext")
        {
        }

        public DbSet<Case> Cases { get; set; }
        public DbSet<Installation> Installations { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; } 

        //Code for unit test
        public void MarkAsModified(Case item)
        {
            Entry(item).State = EntityState.Modified;
        }

        public void MarkAsModified(Installation item)
        {
            Entry(item).State = EntityState.Modified;
        }

        public void MarkAsModified(Position item)
        {
            Entry(item).State = EntityState.Modified;
        }

        public void MarkAsModified(User item)
        {
            Entry(item).State = EntityState.Modified;
        }

        public void MarkAsModified(Notification item)
        {
            Entry(item).State = EntityState.Modified;
        }
    }
}
