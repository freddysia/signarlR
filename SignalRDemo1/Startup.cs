using Microsoft.Owin;
using Owin;
using SignalRDemo1.RabbitMq;

[assembly: OwinStartup(typeof(SignalRDemo1.Startup))]

namespace SignalRDemo1
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.Use(typeof(RabbitMQMiddleware));
            app.MapSignalR();
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
