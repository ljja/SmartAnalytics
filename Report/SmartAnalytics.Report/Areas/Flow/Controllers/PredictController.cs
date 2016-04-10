using System;
using System.Web.Mvc;
using SmartAnalytics.Report.Filters;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;

namespace SmartAnalytics.Report.Areas.Flow.Controllers
{
    /// <summary>
    /// 流量趋势
    /// </summary>
    [UserAuthorization]
    public class PredictController : Controller
    {
        private readonly IFlowService _flowService = new FlowService();
        private readonly IDomainService _domainService = new DomainService();

        public ActionResult Index(string domain = "www.baidu.com", string date = "")
        {
            if (string.IsNullOrEmpty(date))
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }

            var currentDate = DateTime.Now;
            try
            {
                currentDate = DateTime.Parse(date);
            }
            catch { }

            if (string.IsNullOrEmpty(domain))
            {
                domain = "www.baidu.com";
            }

            //加载真实流量数据
            var pageResult = _flowService.GetTimeSpanByHourList(domain, currentDate);

            ViewBag.SiteDomain = domain;
            ViewBag.Date = currentDate.ToString("yyyy-MM-dd");
            ViewBag.AllDomain = _domainService.GetAllList();

            return View(pageResult);
        }
    }
}