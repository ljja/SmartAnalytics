using System.Web.Mvc;
using SmartAnalytics.Report.Filters;

namespace SmartAnalytics.Report.Areas.Origin.Controllers
{
    [UserAuthorization]
    public class HomeController : Controller
    {
        // GET: Origin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}