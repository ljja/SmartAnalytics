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
    /// 按日期段统计
    /// </summary>
    [UserAuthorization]
    public class TimeSpanByDayController : Controller
    {
        private readonly IFlowService _flowService = new FlowService();
        private readonly IDomainService _domainService = new DomainService();

        public ActionResult Index(string domain = "www.baidu.com", string beginDate = "", string endDate = "")
        {
            if (string.IsNullOrEmpty(domain))
            {
                domain = "www.baidu.com";
            }

            ViewBag.Title = string.Format("{0} 至 {1} 日段统计", beginDate, endDate);
            ViewBag.SiteDomain = domain;
            ViewBag.BeginDate = beginDate;
            ViewBag.EndDate = endDate;
            ViewBag.AllDomain = _domainService.GetAllList();
            ViewBag.CurrentPath = "/Flow/TimeSpanByDay";

            var pageResult = _flowService.GetTimeSpanByDayList(domain, DateTime.Parse(beginDate), DateTime.Parse(endDate));

            //关键指标
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

            return View(pageResult);
        }
    }
}