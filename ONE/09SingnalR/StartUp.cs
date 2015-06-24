
using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(Microsoft.AspNet.SignalR.StockTicker.Startup), "Configuration")]
namespace Microsoft.AspNet.SignalR.StockTicker
{

    public static class Startup
    {
        public static void ConfigureSignalR(IAppBuilder app)
        {
            app.MapSignalR();
        }

        public static void Configuration(IAppBuilder app)
        {
            Microsoft.AspNet.SignalR.StockTicker.Startup.ConfigureSignalR(app);
        }
    }
}
