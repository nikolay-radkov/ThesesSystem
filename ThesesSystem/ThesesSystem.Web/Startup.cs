using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ThesesSystem.Web.Startup))]
namespace ThesesSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
