using System.Linq;
using System.Web.Mvc;
using SmartAnalytics.Report.Areas.Flow.Models;
using SmartAnalytics.Report.Filters;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;

namespace SmartAnalytics.Report.Areas.Flow.Controllers
{
    /// <summary>
    /// 实时访客
    /// </summary>
    [UserAuthorization]
    public class RealTimeController : Controller
    {
        private readonly IDomainService _domainService = new DomainService();
        private readonly IFlowService _flowService = new FlowService();

        public ActionResult Index(string domain = "www.baidu.com", int minute=30)
        {
            if (string.IsNullOrEmpty(domain))
            {
                domain = "www.baidu.com";
            }

            var pageResult = _flowService.GetTimeSpanByMinuteList(domain, minute);

            if (pageResult.Any())
            {
                ViewBag.PrimaryIndicators = new PrimaryIndicators
                {
                    PageView = pageResult.Sum(p => p.PageView),
                    UniqueUser = pageResult.Sum(p => p.UniqueUser),
                    NewUniqueUser = pageResult.Sum(p => p.NewUniqueUser),
                    NewUniqueUserRate = pageResult.Average(p => p.NewUniqueUserRate),
                    UniqueIp = pageResult.Sum(p => p.UniqueIp),
                    AccessNumber = pageResult.Sum(p => p.AccessNumber),
                    UserViewPageAverage = (int)pageResult.Average(p => p.UserViewPageAverage),
                    ViewPageDeptAverage = (int)pageResult.Average(p => p.ViewPageDeptAverage),
                    ViewPageTimeSpanAverage = (int)pageResult.Average(p => p.ViewPageTimeSpanAverage),
                    BounceRate = pageResult.Average(p => p.BounceRate),
                };
            }
            else
            {
                ViewBag.PrimaryIndicators = new PrimaryIndicators();
            }

            ViewBag.Minute = minute;
            ViewBag.SiteDomain = domain;
            ViewBag.AllDomain = _domainService.GetAllList();

            return View();
        }

    }
}