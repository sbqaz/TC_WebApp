using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StoreApp.Models;
using WebLib.Models;

namespace API.Models
{
    public class APIContext : DbContext, IStoreAppContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public APIContext() : base("APIContext")
        {
        }

        public System.Data.Entity.DbSet<WebLib.Models.Case> Cases { get; set; }

        public System.Data.Entity.DbSet<WebLib.Models.Installation> Installations { get; set; }

        public System.Data.Entity.DbSet<WebLib.Models.Position> Positions { get; set; }

        public void MarkAsModified(Case item)
        {
            Entry(item).State = EntityState.Modified;
        }

        public System.Data.Entity.DbSet<WebLib.Models.User> Users { get; set; }
    }
}
