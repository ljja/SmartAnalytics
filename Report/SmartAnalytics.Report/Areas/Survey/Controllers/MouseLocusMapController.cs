using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAnalytics.Report.Areas.Survey.Models;
using SmartAnalytics.Report.Filters;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Report.Areas.Survey.Controllers
{
    [UserAuthorization]
    public class MouseLocusMapController : Controller
    {
        private readonly IDomainService _domainService = new DomainService();
        private readonly IUrlMapService _urlMapService = new UrlMapService();

        public ActionResult Index(string domain = "www.baidu.com", int pageIndex = 0, int pageSize = 20)
        {
            if (string.IsNullOrEmpty(domain))
            {
                domain = "www.baidu.com";
            }

            ViewBag.SiteDomain = domain;
            ViewBag.AllDomain = _domainService.GetAllList();

            var paging = new Paging { PageIndex = pageIndex, PageSize = pageSize };

            var queryResult = _urlMapService.GetList(paging, domain);

            return View(queryResult);
        }

        public ActionResult Render(string url, string beginTime, string endTime, int screenWidth = 1024, int screenHeight = 768)
        {
            if (string.IsNullOrEmpty(beginTime))
            {
                beginTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }

            if (string.IsNullOrEmpty(endTime))
            {
                endTime = DateTime.Now.ToString("yyyy-MM-dd");
            }

            ViewBag.BeginTime = DateTime.Parse(beginTime).ToString("yyyy-MM-dd");
            ViewBag.EndTime = DateTime.Parse(endTime).ToString("yyyy-MM-dd");
            ViewBag.Url = url;
            ViewBag.UrlEncode = Server.UrlEncode(url);
            ViewBag.ScreenWidth = screenWidth;
            ViewBag.ScreenHeight = screenHeight;

            return View();
        }

        [HttpGet]
        [ValidateModelState]
        public ActionResult RenderImage(MouseLocusMapRenderImageRequest model)
        {
            return new ContentResult();
        }
    }
}