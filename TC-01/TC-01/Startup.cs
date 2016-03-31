using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TC_01.Startup))]
namespace TC_01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
