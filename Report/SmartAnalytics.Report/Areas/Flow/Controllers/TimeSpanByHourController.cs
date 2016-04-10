using System;
using System.Linq;
using System.Web.Mvc;
using SmartAnalytics.Report.Areas.Flow.Models;
using SmartAnalytics.Report.Filters;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;

namespace SmartAnalytics.Report.Areas.Flow.Controllers
{
    /// <summary>
    /// 按时间段统计
    /// </summary>
    [UserAuthorization]
    public class TimeSpanByHourController : Controller
    {
        private readonly IFlowService _flowService = new FlowService();
        private readonly IDomainService _domainService = new DomainService();

        public ActionResult Index(string domain = "www.baidu.com", string date = "")
        {
            var currentDate = DateTime.Now;
            try
            {
                currentDate = DateTime.Parse(date);
            }
            catch { }

            var dateSpan = DateTime.Now - currentDate;
            if (dateSpan.Days == 0)
            {
                ViewBag.Title = "今日统计";
            }
            else if (dateSpan.Days == 1)
            {
                ViewBag.Title = "昨日统计";
            }
            else
            {
                ViewBag.Title = string.Format("{0}统计", currentDate.ToString("yyyy-MM-dd"));
            }

            if (string.IsNullOrEmpty(domain))
            {
                domain = "www.baidu.com";
            }

            var pageResult = _flowService.GetTimeSpanByHourList(domain, currentDate);

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

            ViewBag.SiteDomain = domain;
            ViewBag.Date = currentDate.ToString("yyyy-MM-dd");
            ViewBag.AllDomain = _domainService.GetAllList();
            ViewBag.CurrentPath = "/Flow/TimeSpanByHour";

            return View(pageResult);
        }
    }
}