using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TC_02_DI.Startup))]
namespace TC_02_DI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
