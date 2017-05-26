using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ServerForGame.Startup))]
namespace ServerForGame
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
