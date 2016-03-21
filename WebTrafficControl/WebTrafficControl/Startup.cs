using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebTrafficControl.Startup))]
namespace WebTrafficControl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
