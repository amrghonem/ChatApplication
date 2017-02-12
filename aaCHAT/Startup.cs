using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(aaCHAT.Startup))]
namespace aaCHAT
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
