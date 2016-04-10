using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAnalytics.Report.Filters;

namespace SmartAnalytics.Report.Areas.Survey.Controllers
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