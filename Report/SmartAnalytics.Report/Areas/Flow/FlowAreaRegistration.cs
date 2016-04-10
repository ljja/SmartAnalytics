using System.Web.Mvc;

namespace SmartAnalytics.Report.Areas.Flow
{
    public class FlowAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Flow";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Flow_default",
                "Flow/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "SmartAnalytics.Report.Areas.Flow.Controllers" }
            );
        }
    }
}