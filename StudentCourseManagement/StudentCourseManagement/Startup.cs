using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentCourseManagement.Startup))]
namespace StudentCourseManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
