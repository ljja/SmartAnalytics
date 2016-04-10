using Microsoft.Owin;
using Owin;
using SmartAnalytics.Report;

[assembly: OwinStartup(typeof(Startup))]

namespace SmartAnalytics.Report
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}