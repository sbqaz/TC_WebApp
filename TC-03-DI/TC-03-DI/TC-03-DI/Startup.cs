using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TC_03_DI.Startup))]
namespace TC_03_DI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
