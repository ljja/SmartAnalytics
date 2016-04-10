using System;
using System.Collections.Generic;
using System.Web.Http;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;
using SmartAnalytics.Services.Models;

namespace SmartAnalytics.Report.ApiControllers
{
    public class TimeSpanByHourController : ApiController
    {
        private readonly IFlowService _flowService = new FlowService();

        /// <summary>
        /// /api/TimeSpanByHour?date=2015-06-10&domain=88mf.com
        /// </summary>
        /// <param name="date"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public IEnumerable<TimeSpanByHourPageItem> Get(string date, string domain = "www.baidu.com")
        {
            if (string.IsNullOrEmpty(date))
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }

            return _flowService.GetTimeSpanByHourList(domain, DateTime.Parse(date));
        }
    }
}
