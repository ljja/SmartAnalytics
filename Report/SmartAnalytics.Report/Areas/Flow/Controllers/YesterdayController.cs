using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WY.TongJi.Report.Filters;

namespace WY.TongJi.Report.Areas.Flow.Controllers
{
    /// <summary>
    /// 昨日按时段流量
    /// </summary>
    [UserAuthorization]
    public class YesterDayController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}