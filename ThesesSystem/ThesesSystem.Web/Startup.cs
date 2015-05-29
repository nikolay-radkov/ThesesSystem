using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ThesesSystem.Web.Startup))]
namespace ThesesSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
