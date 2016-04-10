using System;
using System.Web.Mvc;
using SmartAnalytics.Report.Filters;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;

namespace SmartAnalytics.Report.Areas.Origin.Controllers
{
    [UserAuthorization]
    public class OriginPageController : Controller
    {
        private readonly IDomainService _domainService = new DomainService();
        private readonly IOriginService _originService = new OriginService();

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

            //TODO
            var queryResult = _originService.GetOriginPageListByDay(domain, DateTime.Parse(currentDate.ToString("yyyy-MM-dd")));

            ViewBag.Date = currentDate.ToString("yyyy-MM-dd");
            ViewBag.SiteDomain = domain;
            ViewBag.AllDomain = _domainService.GetAllList();

            return View(queryResult);
        }
    }
}