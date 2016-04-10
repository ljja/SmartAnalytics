using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAnalytics.Report.Filters;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Report.Areas.Survey.Controllers
{
    /// <summary>
    /// 用户轨迹
    /// </summary>
    [UserAuthorization]
    public class UserLocusController : Controller
    {
        private readonly IDomainService _domainService = new DomainService();
        private readonly ISurveyService _surveyService = new SurveyService();

        public ActionResult Index(string domain = "www.baidu.com", string date = "", int pageIndex = 0, int pageSize = 100)
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

            ViewBag.Date = currentDate.ToString("yyyy-MM-dd");
            ViewBag.SiteDomain = domain;
            ViewBag.AllDomain = _domainService.GetAllList();

            var paging = new Paging { PageIndex = pageIndex, PageSize = pageSize };
            var queryResult = _surveyService.GetList(paging, domain);
            return View(queryResult);

           // return View();
        }
       /* public ActionResult Index(int pageIndex = 0, int pageSize = 100)
        {
            var paging = new Paging { PageIndex = pageIndex, PageSize = pageSize };
            return View(_userManageService.GetList(paging));
        }*/
    }
}

