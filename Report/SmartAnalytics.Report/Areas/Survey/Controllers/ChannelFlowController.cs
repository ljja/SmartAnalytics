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
    /// 页面上下游
    /// </summary>
    [UserAuthorization]
    public class ChannelFlowController : Controller
    {
        private readonly IDomainService _domainService = new DomainService();

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

            ViewBag.Date = currentDate.ToString("yyyy-MM-dd");
            ViewBag.SiteDomain = domain;
            ViewBag.AllDomain = _domainService.GetAllList();

            return View();
        }
    }
}