using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAnalytics.Report.Filters;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;

namespace SmartAnalytics.Report.Areas.Survey.Controllers
{
    /// <summary>
    /// 受访域名
    /// </summary>
    [UserAuthorization]
    public class SurveyDomainController : Controller
    {
        private readonly ISurveyService _surveyService = new SurveyService();

        public ActionResult Index(string date = "")
        {
            //默认显示昨天数据
            var currentDate = DateTime.Now.AddDays(-1);
            try
            {
                currentDate = DateTime.Parse(date);
            }
            catch { }

            var queryPageResult = _surveyService.GetSurveyDomainByDayList(DateTime.Parse(currentDate.ToString("yyyy-MM-dd")));

            ViewBag.Date = currentDate.ToString("yyyy-MM-dd");

            return View(queryPageResult);
        }
    }
}