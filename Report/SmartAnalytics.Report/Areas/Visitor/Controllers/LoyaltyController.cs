using System;
using System.Web.Mvc;
using SmartAnalytics.Report.Filters;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;

namespace SmartAnalytics.Report.Areas.Visitor.Controllers
{
    /// <summary>
    /// 忠诚度
    /// </summary>
    [UserAuthorization]
    public class LoyaltyController : Controller
    {
        private readonly IDomainService _domainService = new DomainService();
        private readonly IVisitorService _visitorService = new VisitorService();
        
        public ActionResult Index(string domain = "www.baidu.com", string date = "")
        {
            if (string.IsNullOrEmpty(domain))
            {
                domain = "www.baidu.com";
            }

            var currentDate = DateTime.Now.AddDays(-1);
            try
            {
                currentDate = DateTime.Parse(date);
            }
            catch { }

            var pageResult = _visitorService.GetVisitorLoyaltyListByDay(domain, DateTime.Parse(currentDate.ToString("yyyy-MM-dd")));
            
            ViewBag.Date = currentDate.ToString("yyyy-MM-dd");
            ViewBag.SiteDomain = domain;
            ViewBag.AllDomain = _domainService.GetAllList();

            return View(pageResult);
        }
    }
}