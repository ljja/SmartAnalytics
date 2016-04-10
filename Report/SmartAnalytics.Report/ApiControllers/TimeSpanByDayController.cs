using System;
using System.Collections.Generic;
using System.Web.Http;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;
using SmartAnalytics.Services.Models;

namespace SmartAnalytics.Report.ApiControllers
{
    public class TimeSpanByDayController : ApiController
    {
        private readonly IFlowService _flowService = new FlowService();

        public IEnumerable<TimeSpanByDayPageItem> Get(string domain = "www.baidu.com", string beginDate = "", string endDate = "")
        {
            if (string.IsNullOrEmpty(beginDate))
            {
                beginDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            }

            if (string.IsNullOrEmpty(endDate))
            {
                endDate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            return _flowService.GetTimeSpanByDayList(domain, DateTime.Parse(beginDate), DateTime.Parse(endDate));
        }
    }
}
