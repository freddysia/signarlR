using Microsoft.Owin.Cors;
using Owin;

namespace SignalRSelfHost
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
