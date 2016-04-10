using System.Web.Mvc;

namespace SmartAnalytics.Report.Areas.Survey
{
    public class SurveyAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Survey";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Survey_default",
                "Survey/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "SmartAnalytics.Report.Areas.Survey.Controllers" }
            );
        }
    }
}