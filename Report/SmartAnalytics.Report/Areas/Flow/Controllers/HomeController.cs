using System.Web.Mvc;
using SmartAnalytics.Report.Filters;

namespace SmartAnalytics.Report.Areas.Flow.Controllers
{
    [UserAuthorization]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}