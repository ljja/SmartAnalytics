using System.Web.Mvc;

namespace SmartAnalytics.Report.Areas.Origin
{
    public class OriginAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Origin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Origin_default",
                "Origin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "SmartAnalytics.Report.Areas.Origin.Controllers" }
            );
        }
    }
}