using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebLib.Models;

namespace WebSite.Identity
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

       /* protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Change the name of the table to be Users instead of AspNetUsers
            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users");
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("Users");
        }*/

       //public DbSet<ApplicationUser.ToDo> ToDoes { get; set; }

        // This is useful if you do not want to tear down the database each time you run the application.
        // You want to create a new database if the Model changes
        // public class MyDbInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>
        public class MyDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                InitializeIdentityForEF(context);
                base.Seed(context);
            }

            private void InitializeIdentityForEF(ApplicationDbContext context)
            {
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                string name = "Admin";
                string password = "123456";
                //string test = "test";

                //Create Role Test and User Test
                //RoleManager.Create(new IdentityRole(test));
                //UserManager.Create(new ApplicationUser() { UserName = test });

                //Create Role Admin if it does not exist
                if (!RoleManager.RoleExists(name))
                {
                    var roleresult = RoleManager.Create(new IdentityRole(name));
                }

                //Create User=Admin with password=123456
                var user = new ApplicationUser();
                user.UserName = name;
                user.PhoneNumber = "Unknown region of the Galaxy";
                user.FirstName = "Dread Pirate";
                user.LastName = "Roberts";
                var adminresult = UserManager.Create(user, password);

                //Add User Admin to Role Admin
                if (adminresult.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, name);
                }
            }
        }

		public System.Data.Entity.DbSet<WebLib.Models.Case> Cases { get; set; }
	}
}
