using System.Web.Mvc;

namespace SmartAnalytics.Report.Areas.Visitor
{
    public class VisitorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Visitor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Visitor_default",
                "Visitor/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "SmartAnalytics.Report.Areas.Visitor.Controllers" }
            );
        }
    }
}