using System;
using System.Linq;
using System.Web.Mvc;
using SmartAnalytics.Report.Areas.Flow.Models;
using SmartAnalytics.Report.Filters;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;

namespace SmartAnalytics.Report.Areas.Origin.Controllers
{
    [UserAuthorization]
    public class CategoryController : Controller
    {
        private readonly IDomainService _domainService = new DomainService();
        private readonly IOriginService _originService = new OriginService();

        public ActionResult Index(string domain = "www.baidu.com", string date = "")
        {
            if (string.IsNullOrEmpty(domain))
            {
                domain = "www.baidu.com";
            }

            var currentDate = DateTime.Now;
            try
            {
                currentDate = DateTime.Parse(date);
            }
            catch { }

            var pageResult = _originService.GetCategoryListByDate(domain, DateTime.Parse(currentDate.ToString("yyyy-MM-dd")));

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

            ViewBag.Date = currentDate.ToString("yyyy-MM-dd");
            ViewBag.SiteDomain = domain;
            ViewBag.AllDomain = _domainService.GetAllList();

            return View(pageResult);
        }
    }
}