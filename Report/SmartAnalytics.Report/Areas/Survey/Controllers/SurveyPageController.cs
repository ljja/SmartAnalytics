using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;

namespace SmartAnalytics.Report.Areas.Survey.Controllers
{
    /// <summary>
    /// 受访页面
    /// </summary>
    public class SurveyPageController : Controller
    {
        private readonly IDomainService _domainService = new DomainService();
        private readonly ISurveyService _surveyService = new SurveyService();

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

            ViewBag.Date = currentDate.ToString("yyyy-MM-dd");
            ViewBag.SiteDomain = domain;
            ViewBag.AllDomain = _domainService.GetAllList();
            //查询结果集：传的是yyyy-mm-dd，只要是日期格式，它会自适应，数据库中可以是yyyymmdd
            var queryResult = _surveyService.GetSurveyPageByDayList(DateTime.Parse(currentDate.ToString("yyyy-MM-dd")), domain);
            return View(queryResult);
        }

    }
}