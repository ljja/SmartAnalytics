using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;
using SmartAnalytics.Services.Models;

namespace SmartAnalytics.Report.ApiControllers
{
    public class VisitorResolutionController : ApiController
    {
        private readonly IVisitorService _visitorService = new VisitorService();

        public IEnumerable<VisitorResolutionByDayPageItem> Get(string domain = "www.baidu.com", string date = "", int top = 20)
        {
            if (string.IsNullOrEmpty(domain))
            {
                domain = "www.baidu.com";
            }

            var currentDate = DateTime.Now.AddDays(-1);
            if (string.IsNullOrEmpty(date) == false)
            {
                try
                {
                    currentDate = DateTime.Parse(date);
                }
                catch { }
            }

            var pageResult = _visitorService.GetVisitorResolutionListByDay(domain, DateTime.Parse(currentDate.ToString("yyyy-MM-dd")));

            return pageResult.Take(20);
        }

    }
}
