using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using TrafficControl.DAL;

namespace AspNetIdentityWithoutADatabase.WebApplication.Models
{
    public class CustomUserManager : UserManager<ApplicationUser>
    {
        public CustomUserManager()
            : base(new CustomUserSore<ApplicationUser>())
        {
        }

	    public override Task<ApplicationUser> FindAsync(string userName, string password)
        {
			TCApi ValidateLogin = new TCApi();
            var taskInvoke = Task<ApplicationUser>.Factory.StartNew(() =>
                {
					if (ValidateLogin.LogIn(userName, password))
                        return new ApplicationUser { Id="NeedsAnId", UserName = "UsernameHere"};
                    return null;
                });

            return taskInvoke;
        }
    }
}